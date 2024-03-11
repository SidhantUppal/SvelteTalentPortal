<script>
    export let title = '';
</script>
<div class="container p-4">
    <h1 class="title">{title}</h1>
    <nav class="nav">
        <slot name="nav"/>
    </nav>
    <slot/>
</div>

<style>
    .nav{
        display: flex;
        justify-content: space-between;
        flex-direction: row;
    }
    .title{
        text-align: center;
    }
</style>
