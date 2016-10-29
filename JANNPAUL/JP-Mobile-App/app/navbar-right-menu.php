<!-- Navbar Right Menu -->
<div class="navbar-custom-menu">
    <ul class="nav navbar-nav"

        <?php include './app/api/notifications.php'; ?>
        
        <!-- User Account Menu -->
        <li class="dropdown user user-menu">
            <!-- Menu Toggle Button -->
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                <!-- The user image in the navbar-->
                <img src="<?php echo $_SESSION[MEMBER_PICTURE]; ?>" class="user-image" alt="Avatar" />
                <!-- hidden-xs hides the username on small devices so only the image appears. -->
                <span class="hidden-xs"><?php echo $_SESSION[MEMBER_DISPLAY_NAME]; ?></span>
            </a>
            <ul class="dropdown-menu">
                <!-- The user image in the menu -->
                <li class="user-header">
                    <img src="<?php echo $_SESSION[MEMBER_PICTURE]; ?>" class="img-circle" alt="Avatar" />
                    <p>
                        <?php echo $_SESSION[MEMBER_DISPLAY_NAME]; ?>
                        <small><?php echo $_SESSION[MEMBER_ROLE]; ?></small>
                        <small>Member since <?php echo utils\dateToString($_SESSION[MEMBER_SINCE]); ?></small>
                    </p>
                </li>
                <!-- Menu Body -->
                <!--
                <li class="user-body">
                  <div class="col-xs-4 text-center">
                    <a href="#">Followers</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href="#">Sales</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href="#">Friends</a>
                  </div>
                </li>
                -->
                <!-- Menu Footer-->
                <li class="user-footer">
                    <div class="pull-left">
                        <a href="./?profile=<?php echo $_SESSION[MEMBER_ID]; ?>" class="btn btn-default btn-flat">Profile</a>
                    </div>
                    <div class="pull-right">
                        <a href="./?sign-out=true" class="btn btn-default btn-flat">Sign out</a>
                    </div>
                </li>
            </ul>
        </li>
        <!-- Control Sidebar Toggle Button -->
        <!--
        <li>
            <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
        </li>
        -->
    </ul>
</div>