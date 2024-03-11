
<!-- BirthdayCard.svelte -->
<script>
    // Dummy data representing upcoming birthdays
    const upcomingBirthdays = [
      { name: 'John Doe', date: 'February 15th' },
      { name: 'Jane Smith', date: 'February 18th' },
      { name: 'Michael Johnson', date: 'February 20th' }
      // Add more birthday objects as needed
    ];
  </script>
  
  <style>

    .card {
      background-color: #f7f7f7; /* Light gray background */
      border: 1px solid #ddd; /* Light gray border */
      border-radius: 8px;
      padding: 20px;
      box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Shadow effect */
      width: 100%; /* Fixed width */
    }
  
    .header {
      font-weight: bold;
      border-bottom: 1px solid #ddd; /* Light gray border */
      padding-bottom: 10px;
      margin-bottom: 10px;
    }
  
    .column {
      display: flex;
      justify-content: space-between;
      margin-bottom: 10px;
    }
  
    .name {
      flex: 1;
      color: #333; /* Dark gray text */
    }
  
    .date {
      flex: 1;
      text-align: right;
      color: #666; /* Medium gray text */
    }
  </style>
  
    <div class="card">
      <div class="header">
        <div class="column">
          <div class="name">S.no</div>
          <div class="name">Employee</div>
          <div class="date">Date</div>
        </div>
      </div>
      <!-- Display each upcoming birthday -->
      {#each upcomingBirthdays as birthday, index}
        <div class="column">
          <div class="name">{index + 1}</div>
          <div class="name">{birthday.name}</div>
          <div class="date">{birthday.date}</div>
        </div>
      {/each}
    </div>
  