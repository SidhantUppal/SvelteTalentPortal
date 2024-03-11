<script>
    let date = "05-02-2024";
    let userName = "Ashwani Bhardwaj";
  
    function editProfile() {
      // Add logic for handling the "Edit profile" action
      console.log("Edit profile clicked");
    }
  
    function logout() {
      // Add logic for handling the "Logout" action
      console.log("Logout clicked");
    }
  </script>
  
  <style>
    /* Add your styles here */
    .container-fluid {
    
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 20px;
        background-color: #5e72e4;
    }
  </style>
  
  <div class="container-fluid">
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <div class="alert alert-success ajax-success-alert" style="display: none">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <span class="ajax-success-alert-message"></span>
      </div>
      <div class="alert alert-danger ajax-danger-alert" style="display: none">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <span class="ajax-danger-alert-message"></span>
      </div>
      <!-- Navbar links -->
      <ul class="navbar-nav align-items-center  mr-md-auto">
        <li class="nav-item d-xl-none">
          <!-- Sidenav toggler -->
          <div class="pr-3 sidenav-toggler sidenav-toggler-dark active" data-action="sidenav-pin" data-target="#sidenav-main">
            <div class="sidenav-toggler-inner">
              <i class="sidenav-toggler-line"></i>
              <i class="sidenav-toggler-line"></i>
              <i class="sidenav-toggler-line"></i>
            </div>
          </div>
        </li>
      </ul>
      <ul class="navbar-nav align-items-center ml-auto ml-md-0 ">
        <div class="media-body ml-2 d-none d-lg-block">
          <div class="mb-0 text-sm font-weight-bold" style="color: white;"> Date : {date}</div>
        </div>
        <li class="nav-item dropdown">
          <a class="nav-link pr-0" href="#" role="button" on:click|preventDefault={() => console.log("Dropdown clicked")}>
            <div class="media align-items-center">
              <span class="avatar avatar-sm rounded-circle">
                <img class="header-image" src="https://talentone.teamtalentelgia.com/images/sidebar_image.png" alt="preview">
              </span>
              <div class="media-body ml-2 d-none d-lg-block">
                <span class="mb-0 text-sm font-weight-bold">{userName}</span>
              </div>
            </div>
          </a>
          <div class="dropdown-menu dropdown-menu-right">
            <div class="dropdown-header noti-title">
              <h6 class="text-overflow m-0">Welcome!</h6>
            </div>
            <a href="#" on:click|preventDefault={editProfile} class="dropdown-item">
              <i class="ni ni-single-02"></i>
              <span>Edit profile</span>
            </a>
            <div class="dropdown-divider"></div>
            <a href="#" on:click|preventDefault={logout} class="dropdown-item logout">
              <i class="ni ni-user-run"></i>
              <span>Logout</span>
            </a>
          </div>
        </li>
      </ul>
    </div>
  </div>
  