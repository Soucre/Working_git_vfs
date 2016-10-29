<!-- Messages: style can be found in dropdown.less-->
<li class="dropdown messages-menu">
    <!-- Menu toggle button -->
    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
        <i class="fa fa-envelope-o"></i>
        <span class="label label-info">2</span>
    </a>
    <ul class="dropdown-menu">
        <li class="header">You have 2 messages</li>
        <li>
            <!-- inner menu: contains the messages -->
            <ul class="menu">
                <li><!-- start message -->
                    <a href="#">
                        <div class="pull-left">
                            <!-- User Image -->
                            <img src="https://pbs.twimg.com/profile_images/378800000257956536/7ea1ba6040a904e4a6f6d24e6494313a_400x400.jpeg" class="img-circle" alt="Avatar" />
                        </div>
                        <!-- Message title and timestamp -->
                        <h4>
                            SAIGONTECHCOM
                            <small><i class="fa fa-clock-o"></i> 15 mins</small>
                        </h4>
                        <!-- The message -->
                        <p>Welcome to <?php echo $_SESSION[APPLICATION_NAME]; ?></p>
                    </a>
                </li><!-- end message -->
                <li><!-- start message -->
                    <a href="#">
                        <div class="pull-left">
                            <!-- User Image -->
                            <img src="https://pbs.twimg.com/profile_images/378800000257956536/7ea1ba6040a904e4a6f6d24e6494313a_400x400.jpeg" class="img-circle" alt="Avatar" />
                        </div>
                        <!-- Message title and timestamp -->
                        <h4>
                            Support Team
                            <small><i class="fa fa-clock-o"></i> 5 mins</small>
                        </h4>
                        <!-- The message -->
                        <p>Drop me a message to get the support</p>
                    </a>
                </li><!-- end message -->
            </ul><!-- /.menu -->
        </li>
        <li class="footer"><a href="#">See All Messages</a></li>
    </ul>
</li><!-- /.messages-menu -->