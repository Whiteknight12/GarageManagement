using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChatBotPageViewmodel: BaseViewModel
    {
        [ObservableProperty]
        private string currentMessage;

        [ObservableProperty]
        private ObservableCollection<MessageItem> messages = new();

        public ChatBotPageViewmodel()
        {
            Messages = new ObservableCollection<MessageItem>();
        }

        [RelayCommand]
        public async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(CurrentMessage)) return;

            var userMessage = new MessageItem { Text = CurrentMessage, IsUser = true };
            Messages.Add(userMessage);

            var reply = await CallAIService(CurrentMessage);
            Messages.Add(new MessageItem { Text = reply, IsUser = false });

            CurrentMessage = string.Empty;
        }

        private async Task<string> CallAIService(string prompt)
        {
            using var client = new HttpClient();
            var apiKey = "AIzaSyBf0xyHSQW2A4Y2Tf6d-0R0GD_8XRz0WcE";
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var json = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            },
                generationConfig = new
                {
                    temperature = 0.7,
                    topK = 40,
                    topP = 0.9,
                    maxOutputTokens = 500,
                    responseMimeType = "text/plain"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(json), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode) return "Đã có lỗi xảy ra";

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);

            var parts = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return parts ?? "Không có phản hồi.";
        }
    }

    public class MessageItem
    {
        public string Text { get; set; }
        public bool IsUser { get; set; }
    }
}
