var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Root endpoint - redirect to sad
app.MapGet("/", () => Results.Redirect("/sad"));

// Sad rat page endpoint
app.MapGet("/sad", () => 
{
    var html = @"<!doctype html>
<html>
<head>
  <meta charset=""utf-8"">
  <meta name=""viewport"" content=""width=device-width,initial-scale=1"">
  <title>Why are you sad?</title>
  <style>
    * {
      margin: 0;
      padding: 0;
      box-sizing: border-box;
    }

    html, body {
      height: 100%;
      width: 100%;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Helvetica Neue', sans-serif;
      display: flex;
      align-items: center;
      justify-content: center;
      overflow: hidden;
    }

    .container {
      width: 100%;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      position: relative;
    }

    .screen {
      position: absolute;
      width: 100%;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      opacity: 1;
      transition: opacity 0.6s ease-out, transform 0.6s ease-out;
      transform: scale(1);
    }

    .screen.hidden {
      opacity: 0;
      transform: scale(0.95);
      pointer-events: none;
    }

    .card {
      background: rgba(255, 255, 255, 0.95);
      backdrop-filter: blur(10px);
      border-radius: 24px;
      padding: 60px 40px;
      box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
      max-width: 500px;
      width: 90%;
      text-align: center;
    }

    .title {
      font-size: 32px;
      font-weight: 700;
      color: #333;
      margin-bottom: 40px;
      letter-spacing: -0.5px;
    }

    .input-wrapper {
      position: relative;
      margin-bottom: 24px;
    }

    input[type=""text""] {
      width: 100%;
      padding: 16px 20px;
      border: 2px solid #e0e0e0;
      border-radius: 12px;
      font-size: 16px;
      font-family: inherit;
      transition: all 0.3s ease;
      background: #f9f9f9;
      color: #333;
    }

    input[type=""text""]:focus {
      outline: none;
      border-color: #667eea;
      background: #fff;
      box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
    }

    input[type=""text""]::placeholder {
      color: #999;
    }

    .send-btn {
      width: 100%;
      padding: 14px 28px;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
      border: none;
      border-radius: 12px;
      font-size: 16px;
      font-weight: 600;
      cursor: pointer;
      transition: all 0.3s ease;
      opacity: 0;
      transform: translateY(10px);
      max-height: 0;
      overflow: hidden;
      pointer-events: none;
    }

    .send-btn.show {
      opacity: 1;
      transform: translateY(0);
      max-height: 50px;
      pointer-events: all;
      animation: slideUp 0.5s cubic-bezier(0.34, 1.56, 0.64, 1);
    }

    .send-btn:hover {
      transform: translateY(-2px);
      box-shadow: 0 10px 30px rgba(102, 126, 234, 0.4);
    }

    .send-btn:active {
      transform: translateY(0);
    }

    @keyframes slideUp {
      from {
        opacity: 0;
        transform: translateY(10px);
      }
      to {
        opacity: 1;
        transform: translateY(0);
      }
    }

    .rat-screen {
      flex-direction: column;
      gap: 30px;
    }

    .rat-message {
      font-size: 32px;
      font-weight: 600;
      color: #333;
      letter-spacing: -0.5px;
    }

    .rat-image {
      width: 100%;
      max-width: 500px;
      height: auto;
      border-radius: 16px;
      box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
      animation: ratAppear 0.8s cubic-bezier(0.34, 1.56, 0.64, 1);
    }

    @keyframes ratAppear {
      from {
        opacity: 0;
        transform: scale(0.8) rotateZ(-5deg);
      }
      to {
        opacity: 1;
        transform: scale(1) rotateZ(0deg);
      }
    }

    .rat-card {
      background: rgba(255, 255, 255, 0.95);
      backdrop-filter: blur(10px);
      border-radius: 24px;
      padding: 80px 60px;
      box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
      max-width: 650px;
      width: 90%;
      display: flex;
      flex-direction: column;
      align-items: center;
    }
  </style>
</head>
<body>
  <div class=""container"">
    <!-- Input Screen -->
    <div class=""screen"" id=""inputScreen"">
      <div class=""card"">
        <h1 class=""title"">Why are you sad?</h1>
        <div class=""input-wrapper"">
          <input 
            type=""text"" 
            id=""sadInput"" 
            placeholder=""Tell me what's on your mind...""
            autocomplete=""off""
          />
        </div>
        <button class=""send-btn"" id=""sendBtn"">Send</button>
      </div>
    </div>

    <!-- Rat Screen -->
    <div class=""screen hidden"" id=""ratScreen"">
      <div class=""rat-card"">
        <p class=""rat-message"">Sorry, I can't help you with that but here's a rat</p>
        <img 
          class=""rat-image"" 
          src=""https://bigrat.monster/media/bigrat.jpg"" 
          alt=""big rat monster""
        />
      </div>
    </div>
  </div>

  <script>
    const sadInput = document.getElementById('sadInput');
    const sendBtn = document.getElementById('sendBtn');
    const inputScreen = document.getElementById('inputScreen');
    const ratScreen = document.getElementById('ratScreen');

    // Show send button when input has text
    sadInput.addEventListener('input', () => {
      if (sadInput.value.trim().length > 0) {
        sendBtn.classList.add('show');
      } else {
        sendBtn.classList.remove('show');
      }
    });

    // Handle send button click
    sendBtn.addEventListener('click', () => {
      // Hide input screen
      inputScreen.classList.add('hidden');
      
      // Show rat screen
      setTimeout(() => {
        ratScreen.classList.remove('hidden');
      }, 100);
    });

    // Allow Enter key to send
    sadInput.addEventListener('keypress', (e) => {
      if (e.key === 'Enter' && sadInput.value.trim().length > 0) {
        sendBtn.click();
      }
    });

    // Focus input on load
    sadInput.focus();
  </script>
</body>
</html>";
    return Results.Content(html, "text/html");
});

app.Run();
