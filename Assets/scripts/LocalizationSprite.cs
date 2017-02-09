using UnityEngine;
using System.Collections;
namespace I2.Loc{
	public partial class Localize {
		SpriteRenderer s_renderer;
		// Use this for initialization
		public void RegisterEvents_Sprite(){
			EventFindTarget+= FindTarget_SpritRenderer;
		}
		void FindTarget_SpritRenderer(){
			FindAndCacheTarget (ref s_renderer, SetFinalTerms_AudioSource, DoLocalize_SpriteRenderer, false, false, false);
		} 
		public void SetFinalTerms_SpriteRenderer(string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			SetFinalTerms (s_renderer.sprite.name, 	null,	out primaryTerm, out secondaryTerm, false);
		}
		public void DoLocalize_SpriteRenderer(string MainTranslation, string SecondaryTranslation)
		{
			Sprite o = s_renderer.sprite;
			if (o == null || o.name != MainTranslation) {
				s_renderer.sprite = FindTranslatedObject<Sprite> (MainTranslation);
			}
		}
	}
}
