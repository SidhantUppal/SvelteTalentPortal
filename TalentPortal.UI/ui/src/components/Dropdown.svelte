<!-- Dropdown.svelte -->
<script>
  // @ts-nocheck
  // @ts-ignore
  import { link } from "svelte-routing";
  import { fade, fly } from "svelte/transition";
  import {logout } from "../lib/store/auth";

</script>

<div
  class="profile-dropdown position-absolute bg-white"
  transition:fade={{ duration: 300 }}
>
  <ul>
    <li>
      <a href="/profile" use:link>Profile</a>
    </li>
    <hr />
    <li>
      <a
        href="#"
        use:link
        on:click={() => {
          logout();
        }}
      >
        Logout
      </a>
    </li>
  </ul>
</div>

<style>
  .profile-dropdown {
    position: absolute;
    width: 240px;
    top: 40px;
    left: 0;
    background-color: white;
    border: 1px solid #ddd;
    padding: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }
</style>
