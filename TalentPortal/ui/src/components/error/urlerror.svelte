<!-- Error.svelte -->
<style>
  .error-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 9999; /* Ensure it's on top of other content */
  }

  .error-container {
    text-align: center;
    background-color: #f8d7da;
    padding: 20px;
    border: 1px solid #dc3545;
    width: 100%;
    height: max-content;
    border-radius: 5px;
    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  }

  .error-message {
    color: #721c24;
    font-weight: bold;
  }
</style>

<div class="error-container">
    <p class="error-message">{errorMessage}</p>
</div>

<script>
  export let errorMessage = "An error occurred!";
</script>
