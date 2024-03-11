<!-- Card.svelte -->
<script>
    // Define a static quote
    let quote = "The only way to do great work is to love what you do. - Steve Jobs";
  </script>
  
  <style>
    .card {
      background-color: #f7f7f7; /* Light gray background */
      border: 1px solid #ddd; /* Light gray border */
      border-radius: 8px;
      padding: 20px;
      box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Shadow effect */
      text-align: center;
      width: 100%; /* Full width */
    }
  
    .quote {
      font-size: 1.2rem;
      color: #333; /* Dark gray text */
      margin-bottom: 20px;
      line-height: 1.5;
    }
  
    .author {
      font-style: italic;
      color: #666; /* Medium gray text */
    }
  </style>
  
  <div class="card">
    <p class="quote">{quote}</p>
    <p class="author">- Unknown</p>
  </div>
  