<script>
    import { link } from "svelte-routing";
    export let title = "";
    export let path = "";
</script>

<svelte:head>
    <title>{title}</title>
    <meta name="description" content=" {title} " />
</svelte:head>

<div class="breadCrumb">
    <div class="row">
        <a href={path} use:link>{title}</a>
    </div>
</div>

<style>
    .breadCrumb {
        padding: 20px 0;
        border-radius: 5px; /* Rounded corners */
    }

    .container {
        max-width: 1200px; /* Limit width for better readability */
        margin: 0 auto; /* Center the content */
    }

    .breadCrumb a {
        color: #007bff;
        text-decoration: none;
        font-size: 17px;
        font-weight: bold;
        transition: color 0.3s ease; /* Smooth color transition */
    }

    .breadCrumb a:hover {
        color: #0056b3; /* Darker color on hover */
        text-decoration: underline;
    }
</style>
