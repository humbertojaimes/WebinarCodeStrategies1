using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace EmotionManager
{
	public class EmotionManager
	{
		public enum EmotionType
		{
			Happiness,
			Sadness,
			Contempt,
			Fear,
			Disgust,
			Neutral,
			Anger
		}
		const string apiKey = "45ae6e14f7ae4c978c13545e6f36ffcd";
		static EmotionServiceClient emotionClient = new EmotionServiceClient(apiKey);


		private static async Task<Emotion[]> GetScores(Stream stream) 
		{
			Emotion[] emotionsResults = await emotionClient.RecognizeAsync(stream);
			if (emotionsResults == null)
				throw new Exception("No se detectaron emociones");
			return emotionsResults;
		}

		static int peopleInPhoto = 0;

		public static async Task<Dictionary<EmotionType, float>> GetAverage(Stream stream)
		{
			Dictionary<EmotionType, float> result = null;
			float happinessSum=0, sadnessSum=0, contemptSum=0, angerSum=0;

			try
			{
				Emotion[] emotionResults = await GetScores(stream);
				peopleInPhoto = emotionResults.Count();

				foreach (var emotion in emotionResults)
				{
					happinessSum += emotion.Scores.Happiness;
					sadnessSum += emotion.Scores.Sadness;
					contemptSum += emotion.Scores.Contempt;
					angerSum += emotion.Scores.Anger;
				}

				result = new Dictionary<EmotionType, float>();
				result.Add(EmotionType.Happiness, happinessSum / peopleInPhoto);
				result.Add(EmotionType.Sadness, sadnessSum / peopleInPhoto);
				result.Add(EmotionType.Contempt, contemptSum / peopleInPhoto);
				result.Add(EmotionType.Anger, angerSum / peopleInPhoto);
			}
			catch (Exception ex)
			{
				throw;
			}
			return result;
		}

		public static Tuple<EmotionType, float> GetHighestEmotion(Dictionary<EmotionType, float> scores)
		{
			var orderedScores = scores.OrderByDescending(score => score.Value);
			var higestScore = orderedScores.ElementAt(0).Value * 100;
			return Tuple.Create(orderedScores.ElementAt(0).Key, higestScore);
		}

	}
}
