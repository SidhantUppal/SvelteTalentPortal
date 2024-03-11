<!-- WorkAnniversaryCard.svelte -->
<script>
    // Dummy data representing current month work anniversaries
    const workAnniversaries = [
      { name: 'John Doe', designation: 'Software Engineer', date: 'February 5th' },
      { name: 'Jane Smith', designation: 'UI Designer', date: 'February 10th' },
      { name: 'Michael Johnson', designation: 'Project Manager', date: 'February 15th' }
      // Add more work anniversary objects as needed
    ];
  </script>
  
 
  
    <div class="card">
      <div class="header">
        <div class="column">
          <div class="name">S.no</div>
          <div class="name">Name</div>
          <div class="designation">Designation</div>
          <div class="date">Date</div>  ``
        </div>
      </div>
      <!-- Display each work anniversary -->
      {#each workAnniversaries as anniversary, index}
        <div class="column">
          <div class="name">{index + 1}</div>
          <div class="name">{anniversary.name}</div>
          <div class="designation">{anniversary.designation}</div>
          <div class="date">{anniversary.date}</div>
        </div>
      {/each}
    </div>
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
    .card {
      background-color: #f7f7f7; /* Light gray background */
      border: 1px solid #ddd; /* Light gray border */
      border-radius: 8px;
      padding: 20px;
      box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Shadow effect */
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
  
    .designation {
      flex: 1;
      color: #333; /* Dark gray text */
      font-style: italic;
    }
  
    .date {
      flex: 1;
      text-align: right;
      color: #666; /* Medium gray text */
    }
  </style>