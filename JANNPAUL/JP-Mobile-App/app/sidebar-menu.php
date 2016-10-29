
<?php
$db = database\getMySqli()
?>
<!-- Sidebar Menu -->
<ul class="sidebar-menu">
    <li class="header">WELCOME</li>
    <!-- Products -->
    <li class="<?php
    if (filter_has_var(INPUT_GET, 'products')) {
        echo ' active';
    }
    ?>">
        <a href="?products"><i class="fa fa-tag"></i> <span>Products</span>
            <?php
            $result = $db->query("SELECT COUNT(*) count from `product`");
            if ($result) {
                $count = $result->fetch_object();
                echo '<span class="label label-info pull-right">' . $count->count . '</span>';
            } else {
                echo "0";
            }
            $result->close();
            ?>
        </a>
    </li>

    <!-- Profile Management -->
    <li class="<?php
    if (filter_has_var(INPUT_GET, 'customer-profiles')) {
        echo ' active';
    }
    ?>">
        <a href="?customer-profiles"><i class="fa fa-users"></i> <span>Registered Users</span>
            <?php
            $result = $db->query("SELECT COUNT(*) count from `customer_profile`");
            if ($result) {
                $count = $result->fetch_object();
                echo '<span class="label label-info pull-right">' . $count->count . '</span>';
            } else {
                echo "0";
            }
            $result->close();
            ?>
        </a>
    </li>
    <!-- Profile Management -->
    <li class="<?php
    if (filter_has_var(INPUT_GET, 'saved-rings')) {
        echo ' active';
    }
    ?>">
        <a href="?saved-rings"><i class="fa fa-save"></i> <span>Saved Rings</span>
            <?php
            $result = $db->query("SELECT COUNT(*) count from `saved_ring`");
            if ($result) {
                $count = $result->fetch_object();
                echo '<span class="label label-info pull-right">' . $count->count . '</span>';
            } else {
                echo "0";
            }
            $result->close();
            ?>
        </a>
    </li>
    <!-- System Administration -->
    <?php if ($_SESSION[MEMBER_ROLE] === "Administrator") { ?>
        <li class="header">SYSTEM</li>
        <li class="treeview<?php
        if (filter_has_var(INPUT_GET, 'users') || filter_has_var(INPUT_GET, 'settings')) {
            echo ' active';
        }
        ?>">
            <a href="#"><i class="fa fa-gears"></i> <span>Administration</span> <i class="fa fa-angle-left pull-right"></i></a>
            <ul class="treeview-menu">
                <li <?php
                if (filter_has_var(INPUT_GET, 'users')) {
                    echo 'class="active"';
                }
                ?>><a href="./?users"><i class="fa fa-users"></i> <span>Users</span>
                    <?php
                    $result = $db->query("SELECT COUNT(*) count from `member`");
                    if ($result) {
                        $count = $result->fetch_object();
                        echo '<span class="label label-info pull-right">' . $count->count . '</span>';
                    } else {
                        echo "0";
                    }
                    $result->close();
                    ?>
                    </a></li>
                <li <?php
                if (filter_has_var(INPUT_GET, 'settings')) {
                    echo 'class="active"';
                }
                ?>><a href="./?settings"><i class="fa fa-gears"></i> Settings</a></li>
            </ul>
        </li>
    <?php } ?>
</ul>