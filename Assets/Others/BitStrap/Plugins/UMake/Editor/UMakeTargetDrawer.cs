using UnityEditor;
using UnityEngine;

namespace BitStrap
{
	[System.AttributeUsage( System.AttributeTargets.Field )]
	public sealed class UMakeTargetActionsAttribute : PropertyAttribute
	{
	}

	[CustomPropertyDrawer( typeof( UMakeTargetActionsAttribute ) )]
	public sealed class UMakeTargetDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			position = position.Right( -16.0f );

			float buttonWidth = 40.0f;
			Rect preButtonRect, buildButtonRect, postButtonRect;

			Rect propertyRect = position
				.Right( buttonWidth, out postButtonRect )
				.Right( buttonWidth, out buildButtonRect )
				.Right( buttonWidth, out preButtonRect );

			BitStrap.EditorHelper.BeginChangeIndentLevel( 0 );

			EditorGUI.PropertyField( propertyRect, property, GUIContent.none );

			var target = property.objectReferenceValue as UMakeTarget;
			var action = UMakeTargetEditor.BuildAction.None;

			GUI.enabled = target != null && UMakeTargetEditor.CanBuild;

			if( GUI.Button( preButtonRect, "Pre", EditorStyles.miniButtonLeft ) )
				action = UMakeTargetEditor.BuildAction.PreActions;

			if( GUI.Button( buildButtonRect, "Build", EditorStyles.miniButtonMid ) )
				action = UMakeTargetEditor.BuildAction.Build;

			if( GUI.Button( postButtonRect, "Post", EditorStyles.miniButtonRight ) )
				action = UMakeTargetEditor.BuildAction.PostActions;

			GUI.enabled = true;

			BitStrap.EditorHelper.EndChangeIndentLevel();

			property.serializedObject.ApplyModifiedProperties();

			UMake umake;
			if( UMake.Get().TryGet( out umake ) && target != null )
				UMakeTargetEditor.ExecuteAction( target, action );
		}
	}
}