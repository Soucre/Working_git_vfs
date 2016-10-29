<!-- @@TITLE=Dashboard-->
<!-- @@DESCRIPTION=Welcome-->
<!-- @@BREADCRUMB=-->
<div class="row">
   
    <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-gray">
            <div class="inner">
                <h3><?php
                    echo \database\getCount("product");
                    ?></h3>
                <p>Products</p>
            </div>
            <div class="icon">
                <i class="ion ion-cube"></i>
            </div>
            <a href="?products" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
        </div>
    </div><!-- ./col -->
    <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-gray">
            <div class="inner">
                <h3><?php
                    echo \database\getCount("customer_profile");
                    ?></h3>
                <p>User Registrations</p>
            </div>
            <div class="icon">
                <i class="ion ion-person-add"></i>
            </div>
            <a href="./?customer-profiles" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
        </div>
    </div><!-- ./col -->
    <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-gray">
            <div class="inner">
                <h3><?php
                    echo \database\getCount("saved_ring");
                    ?></h3>
                <p>Rings Saved</p>
            </div>
            <div class="icon">
                <i class="ion ion-pie-graph"></i>
            </div>
            <a href="./?saved-rings" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
        </div>
    </div><!-- ./col -->
</div>
<div class="row">
    <div class="col-md-12">
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Activities</h3>
                <div class="box-tools pull-right">
                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div><!-- /.box-header -->
            <div class="box-body">
                <ul class="products-list product-list-in-box">
                    <?php
                    $notification_info_query = $db->query("SELECT  notification_event.time, notification_event.title, notification_event.description,
                            `customer_profile`.`fname`,`notification_event`.udid
                            FROM `notification_event`
                                JOIN `customer_profile` on `customer_profile`.udid = `notification_event`.udid
                            ORDER BY  `time` DESC 
                            LIMIT 4");
                    if ($notification_info_query) {
                        if ($notification_info_query->num_rows <= 0) {
                            echo "(No events)";
                        } else {
                            while ($notification = $notification_info_query->fetch_object()) {
                                ?>
                                <li class="item">
                                    <div >
                                        <span class="product-title"><?php echo htmlspecialchars($notification->title); ?></span>
                                        <span class="product-description">
                                            <?php echo "<b><a href=\"./?customer-profile=$notification->mem_id\">$notification->fname</a>: </b>"; ?>
                                            <?php echo $notification->description; ?>
                                            <br>
                                            <?php echo \utils\humanTiming($notification->time); ?>
                                        </span>
                                    </div>
                                </li><!-- /.item -->
                                <?php
                            }
                            $notification_info_query->close();
                        }
                    }
                    ?>
                </ul>
            </div><!-- /.box-body -->
            
        </div>
    </div>
</div>

<!--
<script>
    $(document).ready(function () {
        $.ajax({
            url: "./app/api/top-products.php",
            dataType: 'jsonp',
            success: function (data) {
                alert(data);
            }
        });

        $.getJSON("./app/api/top-products.php", function (data) {
            alert("done");
            // Get the context of the canvas element we want to select
            var ctx = document.getElementById("pieChart").getContext("2d");
            var myNewChart = new Chart(ctx).Doughnut(data);
        });
    });

</script>
-->