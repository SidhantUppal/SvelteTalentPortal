<!-- ProjectCard.svelte -->
<script>
  export let title = "Total Assigned Projects";
  export let value = 9;
</script>

<div class="col">
  <div class="content">
    <h5 class="card-title text-uppercase text-muted mb-2">{title}</h5>
    <span class="h2 font-weight-bold mb-0">{value}</span>
  </div>
</div>

<style>
  .col {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 1px solid #ddd;
    border-radius: 8px;
    overflow: hidden;
    margin: 16px;
    width: 200px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    background-color: #fff;
  }

  .content {
    text-align: center;
    padding: 16px;
  }

  .card-title {
    font-size: 14px;
    color: #6c757d;
    margin-bottom: 8px;
  }

  .h2 {
    font-size: 24px;
    font-weight: bold;
    color: #000;
  }

  .mb-0 {
    margin-bottom: 0;
  }
</style>
