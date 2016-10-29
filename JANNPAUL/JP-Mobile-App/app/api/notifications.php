<?php
$notification_count =  database\getCount('notification_event');
?>

<!-- Messages: style can be found in dropdown.less-->
<li class="dropdown messages-menu">
    <!-- Menu toggle button -->
    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
        <i class="fa fa-bell-o"></i>
        <?php if ($notification_count > 0) echo "<span class=\"label label-danger\">$notification_count</span>"; ?>
    </a>
    <ul class="dropdown-menu">
        <?php if ($notification_count > 0) echo "<li class=\"header\">You have $notification_count notifications</li>"; ?>
        <li>
            <!-- inner menu: contains the messages -->
            <ul class="menu">

                <?php
                $db = \database\getMySqli();
                $result = $db->query("SELECT  * from `notification_event`
                    ORDER BY  `time` DESC 
                    LIMIT 20");
                if ($result) {
                    while ($notification = $result->fetch_object()) {
                        ?>
                        <li><!-- start message -->
                            <a href="#">
                                <div class="pull-left">
                            <img src="./dist/img/avatar.png" class="img-circle" alt="User Image">
                          </div>
                                <!-- Message title and timestamp -->
                                <h4>
                                    <?php echo htmlspecialchars(substr($notification->title, 0, 50)); ?>
                                    <small><i class="fa fa-clock-o"></i> <?php echo \utils\humanTiming($notification->time); ?></small>
                                </h4>
                                <!-- The message -->
                                <p><?php echo htmlspecialchars(substr($notification->description, 0, 100)); ?></p>
                            </a>
                        </li><!-- end message -->

                        <?php
                    }
                }
                ?>
            </ul><!-- /.menu -->
        </li>
        <!--<li class="footer"><a href="#">See All Notifications</a></li>-->
    </ul>
</li><!-- /.messages-menu -->
