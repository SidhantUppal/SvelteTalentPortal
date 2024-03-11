<!-- HolidaysCard.svelte -->
<script>
    // Dummy data representing current month holidays
    const holidays = [
      { name: 'New Year\'s Day', date: 'January 1st' },
      { name: 'Martin Luther King Jr. Day', date: 'January 17th' },
      { name: 'President\'s Day', date: 'February 21st' }
      // Add more holiday objects as needed
    ];
  </script>
  
  <style>
    .card {
      background-color: #f7f7f7; /* Light gray background */
      border: 1px solid #ddd; /* Light gray border */
      border-radius: 8px;
      padding: 20px;
      box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Shadow effect */
      width: 100%; /* Full width */
    }
  
    .header {
      font-weight: bold;
      border-bottom: 1px solid #ddd; /* Light gray border */
      padding-bottom: 10px;
      margin-bottom: 10px;
    }
  
    .row {
      display: flex;
      justify-content: space-between;
      margin-bottom: 10px;
    }
  
    .column {
      flex: 1;
    }
  
    .name {
      color: #333; /* Dark gray text */
    }
  
    .date {
      color: #666; /* Medium gray text */
      text-align: right;
    }
  </style>
  
  <div class="card">
    <div class="header">
      <div class="row">
        <div class="column name">S.no</div>
        <div class="column name">Name</div>
        <div class="column date">Date</div>
      </div>
    </div>
    <!-- Display each holiday -->
    {#each holidays as holiday, index}
      <div class="row">
        <div class="column name">{index + 1}</div>
        <div class="column name">{holiday.name}</div>
        <div class="column date">{holiday.date}</div>
      </div>
    {/each}
  </div>
  