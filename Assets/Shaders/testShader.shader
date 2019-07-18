// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33374,y:32766,varname:node_9361,prsc:2|custl-4733-OUT,alpha-7042-A;n:type:ShaderForge.SFN_Slider,id:5328,x:31529,y:33056,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:7042,x:32744,y:32801,ptovrint:False,ptlb:node_7042,ptin:_node_7042,varname:node_7042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8e12b01a3c4a545ecb549a9b3c7670cd,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Add,id:4733,x:33115,y:32819,varname:node_4733,prsc:2|A-7042-RGB,B-2703-OUT;n:type:ShaderForge.SFN_Multiply,id:5083,x:32795,y:33058,varname:node_5083,prsc:2|A-9923-OUT,B-9268-RGB,C-125-OUT;n:type:ShaderForge.SFN_Fresnel,id:9923,x:32546,y:32911,varname:node_9923,prsc:2;n:type:ShaderForge.SFN_Color,id:9268,x:32520,y:33090,ptovrint:False,ptlb:node_9268,ptin:_node_9268,varname:node_9268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:125,x:32592,y:33284,varname:node_125,prsc:2,v1:2;n:type:ShaderForge.SFN_Slider,id:3766,x:32827,y:33234,ptovrint:False,ptlb:node_3766,ptin:_node_3766,varname:node_3766,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2703,x:33001,y:33076,varname:node_2703,prsc:2|A-5083-OUT,B-3766-OUT;proporder:5328-7042-9268-3766;pass:END;sub:END;*/

Shader "Shader Forge/testShader" {
    Properties {
        _Gloss ("Gloss", Range(0, 1)) = 0.5
        _node_7042 ("node_7042", 2D) = "black" {}
        _node_9268 ("node_9268", Color) = (0,0,1,1)
        _node_3766 ("node_3766", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_7042; uniform float4 _node_7042_ST;
            uniform float4 _node_9268;
            uniform float _node_3766;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 _node_7042_var = tex2D(_node_7042,TRANSFORM_TEX(i.uv0, _node_7042));
                float3 finalColor = (_node_7042_var.rgb+(((1.0-max(0,dot(normalDirection, viewDirection)))*_node_9268.rgb*2.0)*_node_3766));
                fixed4 finalRGBA = fixed4(finalColor,_node_7042_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
