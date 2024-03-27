Shader "Custom/UI/Flow2"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        _Angle("Angle", Range(1, 89)) = 75					// ��б�Ƕ�
        _Width("Width", Range(0.1, 1)) = 0.25				// ������
        _Interval("Interval", Int) = 3						// ���
        _Duration("duration", Float) = 1.5					// ����ʱ��
        _FlowColor("Flow Color", Color) = (1, 1, 1, 1)		// ������ɫ

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask[_ColorMask]

            Pass
            {
                Name "Default"
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                fixed4 _Color;
                fixed4 _TextureSampleAdd;
                float4 _ClipRect;
                float4 _MainTex_ST;

                float _Angle;
                fixed _Width;
                int _Interval;
                float _Duration;
                fixed4 _FlowColor;

                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                    OUT.color = v.color * _Color;
                    return OUT;
                }

                // ������ʵ��: http://blog.sina.com.cn/s/blog_471132920101d8zf.html
                //�������ʹ����� frag����֮ǰ�������޷�ʶ��
                //���ģ����㺯�����Ƕȣ�uv,�����x���ȣ��������ʼʱ�䣬ƫ�ƣ�����ѭ��ʱ��
                float inFlash(float angle,float2 uv,float xLength,int interval,int beginTime, float offX, float loopTime)
                {
                    //����ֵ
                    float brightness = 0;

                    //��б��
                    float angleInRad = 0.0174444 * angle;

                    //��ǰʱ��
                    float currentTime = _Time.y;

                    //��ȡ���ι��յ���ʼʱ��
                    int currentTimeInt = _Time.y / interval;
                    currentTimeInt *= interval;

                    //��ȡ���ι��յ�����ʱ�� = ��ǰʱ�� - ��ʼʱ��
                    float currentTimePassed = currentTime - currentTimeInt;
                    if (currentTimePassed > beginTime)
                    {
                        //�ײ���߽���ұ߽�
                        float xBottomLeftBound;
                        float xBottomRightBound;

                        //�˵�߽�
                        float xPointLeftBound;
                        float xPointRightBound;

                        float x0 = currentTimePassed - beginTime;
                        x0 /= loopTime;

                        //�����ұ߽�
                        xBottomRightBound = x0;

                        //������߽�
                        xBottomLeftBound = x0 - xLength;

                        //ͶӰ��x�ĳ��� = y/ tan(angle)
                        float xProjL;
                        xProjL = (uv.y) / tan(angleInRad);

                        //�˵����߽� = �ײ���߽� - ͶӰ��x�ĳ���
                        xPointLeftBound = xBottomLeftBound - xProjL;
                        //�˵���ұ߽� = �ײ��ұ߽� - ͶӰ��x�ĳ���
                        xPointRightBound = xBottomRightBound - xProjL;

                        //�߽����һ��ƫ��
                        xPointLeftBound += offX;
                        xPointRightBound += offX;

                        //����õ���������
                        if (uv.x > xPointLeftBound && uv.x < xPointRightBound)
                        {
                            //�õ�������������ĵ�
                            float midness = (xPointLeftBound + xPointRightBound) / 2;

                            //�������ĵ�ĳ̶ȣ�0��ʾλ�ڱ�Ե��1��ʾλ�����ĵ�
                            float rate = (xLength - 2 * abs(uv.x - midness)) / (xLength);
                            brightness = rate;
                        }
                    }
                    brightness = max(brightness,0);

                    //������ɫ = ����ɫ * ����
                    float4 col = float4(1,1,1,1) * brightness;
                    return brightness;
                }

                // �Ƕ�, uv, ������(0~1), �������⿪ʼ�ļ��ʱ��, ������������һ������ͼƬ��ʱ��
                fixed inFlow(float angle, float2 uv, fixed width, int interval, float duration)
                {
                    float rad = angle * 0.0174444;
                    float tanRad = tan(rad);

                    float maxYProj2X = 1.0 / tanRad;
                    float totalMovX = 1 + width + maxYProj2X;

                    float totalTime = interval + duration;
                    int cnt = _Time.y / totalTime;
                    float currentTime = _Time.y - cnt * totalTime;

                    fixed flow = 0;
                    if (currentTime < duration)
                    {
                        fixed x0 = currentTime / (duration / totalMovX);
                        float yProj2X = uv.y / tanRad;
                        float xLeft = x0 - width - yProj2X;
                        float xRight = xLeft + width;
                        float xMid = 0.5 * (xLeft + xRight);
                        flow = step(xLeft, uv.x) * step(uv.x, xRight);
                        // ��ֵ�����������ĵľ���ı�������������
                        flow *= (width - 2 * abs(uv.x - xMid)) / width;
                    }
                    return flow;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                    #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                    #endif

                    #ifdef UNITY_UI_ALPHACLIP
                    clip(color.a - 0.001);
                    #endif

                    //����i.uv�Ȳ������õ�����ֵ
                   //float flow = inFlash(30, IN.texcoord, 0.5, 5/*interval*/, 2/*beginTime*/, 0/*xOffset*/, 2/*loopTime*/);

                   fixed flow = inFlow(_Angle, IN.texcoord, _Width, _Interval, _Duration);
                   color += _FlowColor * flow * step(0.5, color.a);

                   return color;
               }
           ENDCG
           }
        }
}

