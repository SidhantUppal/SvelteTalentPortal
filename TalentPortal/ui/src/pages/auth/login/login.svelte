<script>
  import { writable } from "svelte/store";
  import {
    CURRENT_LAYOUT_KEY,
    currentLayout,
    getCurrentUser,
    login,
  } from "../../../lib/store/auth";
  import { link, navigate } from "svelte-routing";
  import Private from "../../../routes/private.routes.svelte";
  import { toast } from "@zerodevx/svelte-toast";

  let username = "";
  let password = "";
  let currentUser = getCurrentUser();
  let isCorrectUser = true;
  $: if (currentUser) {
    navigate("/dashboard");
  }
  function handleSubmit() {
    isCorrectUser = true;
    login(username, password).then((res) => {
      if (!res) {
        isCorrectUser = false;
      } else {
        currentLayout.set("dashboard");
        localStorage.setItem(CURRENT_LAYOUT_KEY, "dashboard");
        isCorrectUser = true;
        currentUser = getCurrentUser();
        window.location.reload();
      }
    });
  }
  $: if (currentUser && currentUser.accessToken) {
    navigate("/dashboard");
  }
  $: if (!isCorrectUser) {
    toast.push("Invalid username or password");
  }
</script>

<div class="login-container">
  <h2 class="login-header">Welcome Back!</h2>
  <form on:submit|preventDefault={handleSubmit} class="login-form">
    <div class="form-group">
      <label for="username" class="form-label">Username:</label>
      <input
        type="text"
        id="username"
        bind:value={username}
        class="form-input"
        required
      />
    </div>
    <div class="form-group">
      <label for="password" class="form-label">Password:</label>
      <input
        type="password"
        id="password"
        bind:value={password}
        class="form-input"
        required
      />
    </div>
    <button type="submit" class="btn-primary">Login</button>
  </form>
  <div class="row justify-content-center p-4">
    <strong class="text-center">Demo Credentials</strong>
    <div class="col-12">
      <p class="text-center">Username: karleyEspinoza@mail.com</p>
      <p class="text-center">Password: 123456</p>
    </div>
  </div>
</div>

<style>
  .login-container {
    max-width: 400px;
    margin: 0 auto;
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
    background-color: #f0f0f0;
  }

  .login-header {
    text-align: center;
    color: #333;
    margin-bottom: 30px;
  }

  .login-form {
    display: flex;
    flex-direction: column;
  }

  .form-group {
    margin-bottom: 20px;
  }

  .form-label {
    font-size: 16px;
    font-weight: bold;
    margin-bottom: 8px;
    color: #333;
  }

  .form-input {
    padding: 12px;
    border: 2px solid #ccc;
    border-radius: 5px;
    font-size: 16px;
    transition: border-color 0.3s;
    box-sizing: border-box;
  }

  .form-input:focus {
    outline: none;
    border-color: #007bff;
  }

  .btn-primary {
    padding: 12px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s;
  }

  .btn-primary:hover {
    background-color: #0056b3;
  }

  .btn-primary:active {
    background-color: #004080;
  }
  .toast-container {
    margin-top: 20px;
    padding: 10px;
    background-color: #ffc107; /* Yellow color for warning */
    border-radius: 5px;
  }

  .toast-message {
    color: #333;
  }
</style>
