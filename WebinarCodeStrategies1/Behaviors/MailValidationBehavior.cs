using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WebinarCodeStrategies1
{
	public class MailValidationBehavior: Behavior<Entry>
	{
		
		const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
		@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

		public MailValidationBehavior()
		{
		}

		static readonly BindablePropertyKey IsValidPropertyKey =
			BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(MailValidationBehavior), false);
			
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid 
		{ 
			get { return (bool)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value);  }	
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged+= Bindable_TextChanged;
		}

		void Bindable_TextChanged(object sender, TextChangedEventArgs e)
		{
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(e.NewTextValue);
			IsValid = match.Success;
			((Entry)sender).TextColor = IsValid ? Color.Black : Color.Red;           
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= Bindable_TextChanged;
		}

	}
}

