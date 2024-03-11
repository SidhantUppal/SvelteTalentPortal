<!-- Toast.svelte -->
<script>
    import { readable, writable } from 'svelte/store';
    /**
     * @param {string} message
     */
     export function showToast(message) {
      toastMessage.set(message);
      toastVisible.set(true);
      
      // Automatically hide the toast after a timeout
      setTimeout(() => {
        hideToast();
      }, 3000); // Adjust the timeout as needed
    }
    // Store for toast visibility
    export const toastVisible = writable(false);
    
    // Store for toast message
    export const toastMessage = writable('');
  
    // Function to show the toast
   
  
    // Function to hide the toast
    export function hideToast() {
      toastVisible.set(false);
    }
  </script>
  
  {#if $toastVisible}
    <div class="toast">
      <span>{ $toastMessage }</span>
      <button on:click={hideToast}>Close</button>
    </div>
  {/if}
  
  <style>
    .toast {
      position: fixed;
      bottom: 20px;
      left: 50%;
      transform: translateX(-50%);
      background-color: #333;
      color: #fff;
      padding: 10px 20px;
      border-radius: 5px;
      box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
      z-index: 999;
    }
  
    button {
      background-color: transparent;
      border: none;
      color: #fff;
      cursor: pointer;
      font-size: 14px;
      margin-left: 10px;
    }
  </style>
  