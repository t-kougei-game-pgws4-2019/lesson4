Shader "Unlit/Water"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BulletX("X",Range(-1,1)) = -1
		_BulletZ("Z", Range(-1, 1)) = -1
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag

			#include "UnityCustomRenderTexture.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _BulletX;
			float _BulletZ;

			float2 frag(v2f_customrendertexture i) :SV_Target
			{
				//波動シュミレーション
				float2 last = tex2D(_SelfTexture2D,i.globalTexcoord);
				float d = 1.0 / _CustomRenderTextureWidth;
				float h1 = tex2D(_SelfTexture2D, i.globalTexcoord + float2(+d, 0.0)).x;
				float h2 = tex2D(_SelfTexture2D, i.globalTexcoord + float2(-d, 0.0)).x;
				float h3 = tex2D(_SelfTexture2D, i.globalTexcoord + float2(0.0, +d)).x;
				float h4 = tex2D(_SelfTexture2D, i.globalTexcoord + float2(0.0, -d)).x;

				float s = 0.3;
				float h = 2.0*last.x - last.y + s * (h1 + h2 + h3 + h4 - 4.0*last.x);

				//弾を打つ
				float bullet_siz = 1.0 / 0.1;
				float2 bullet_center = float2(0.5, 0.5);
				float bullet = 0.01*tex2D(_MainTex, i.globalTexcoord*bullet_siz - float2(_BulletX, _BulletZ)*bullet_siz + bullet_center).x;

				h += bullet;

				//波を抑える
				h *= 0.9999;

				return float2(h, last.x);
			}
			ENDCG
		}
	}
}
