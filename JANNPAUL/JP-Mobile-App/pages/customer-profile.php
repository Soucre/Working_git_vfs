<!-- @@TITLE=(New Registered User)-->
<!-- @@DESCRIPTION=Please fill in profile's information-->
<!-- @@BREADCRUMB=Profile Management#<a href="?customer-profiles">Registered Users</a>#Registered User-->
<?php
$profile_id = 0;
$db = database\getMySqli();

if (filter_has_var(INPUT_POST, "submit")) {
    //Save mode
    $profile_id = filter_input(INPUT_POST, "form-profile-id");
    // Save mode
    $email = $db->real_escape_string(filter_input(INPUT_POST, "email"));
    $fname = $db->real_escape_string(filter_input(INPUT_POST, "fname"));
    $lname = $db->real_escape_string(filter_input(INPUT_POST, "lname"));
    $gender = $db->real_escape_string(filter_input(INPUT_POST, "gender"));
    $city = $db->real_escape_string(filter_input(INPUT_POST, "city"));
    $country = $db->real_escape_string(filter_input(INPUT_POST, "country"));
    $contact = $db->real_escape_string(filter_input(INPUT_POST, "contact"));

    //Picture uploading
    $uploadOk = false;
    if ($_FILES["picture"]["name"] && $_FILES["picture"]["tmp_name"]) {
        $target_dir = "dist/img/avatar/";
        $target_file = $target_dir . basename($_FILES["picture"]["name"]);
        $imageFileType = pathinfo($target_file, PATHINFO_EXTENSION);
        $check = getimagesize($_FILES["picture"]["tmp_name"]);
        $file_msg = "";
        if ($check) {
            $uploadOk = true;
            // Check file size
            if ($_FILES["picture"]["size"]> 3145728) {
                $file_msg .= "<br>Picture file size is too large.";
                $uploadOk = false;
            }
            // Allow certain file formats
            if ($imageFileType != "jpg" && $imageFileType != "png" && $imageFileType != "jpeg" && $imageFileType != "gif") {
                $file_msg .= "<br>Sorry, only JPG, JPEG, PNG & GIF files are allowed.";
                $uploadOk = false;
            }
            // Check if $uploadOk is set to 0 by an error
            if (!$uploadOk) {
                $file_msg .= "<br>(Your picture was not uploaded).";
                // if everything is ok, try to upload file
            } else {
                if (move_uploaded_file($_FILES["picture"]["tmp_name"], $target_file)) {
                    //OK, no need message              
                    // call the function that will create the thumbnail. The function will get as parameters 
                    //the image name, the thumbnail name and the width and height desired for the thumbnail
                    $thumb = \image_utils\make_thumb($target_file, $target_file, 100, 100);
                } else {
                    $file_msg .= "<br>Sorry, there was an error uploading your file.";
                    $uploadOk = false;
                }
            }
        } else {
            $file_msg .= "<br>File is not an image.";
        }
    }
    $set_statement = "SET `email` = '$email', `fname` = '$fname', `lname`='$lname', gender='$gender', city='$city', `country` = '$country',`contact` = '$contact'";
    if ($uploadOk && file_exists($target_file)) {
        $set_statement .= ", picture = '$target_file'";
    } else {
        $uploadOk = false;
    }
    if ($profile_id == 0) {
        // Create new
        $sql = "INSERT INTO `customer_profile` $set_statement";
    } else {
        // Save current
        $sql = "UPDATE `customer_profile` $set_statement WHERE `mem_id` = $profile_id";
    }
    $db->query($sql);
    if ($db->affected_rows >= 1) {
        if ($profile_id == 0) {
            $profile_id = $db->insert_id;
        }
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($fname) . "'s profile has been saved.";
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "success";
    } else {
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($fname) . "'s profile has not been saved." . $file_msg;
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "warning";
    }
    $_SESSION[APPLICATION_MESSAGE] .= " What do you want to do next?<br>"
            . "Continue editing or go back to <a href=\"./?customer-profiles\">Registered Users</a>.";
} else {
    $profile_id = filter_input(INPUT_GET, "customer-profile");
}

$found = false;
if ($profile_id > 0) {
    $result = $db->query("SELECT * FROM `customer_profile` WHERE `mem_id` = $profile_id");
    if ($result) {
        $user = $result->fetch_object();
        $found = true;
    }
    if (!$found) {
        $_SESSION[APPLICATION_MESSAGE] = "Profile #$profile_id not found. You may not have the right to view, or it has been deleted.";
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "danger";
    }
    $result->close();
    echo '<script>contentHeaderTitle_SetContent("' . htmlspecialchars($user->fname . ' ' . $user->lname) . '<small>Profile of registered user</small>")</script>';
}

include './app/form-notification.php';
?>

<form role="form" action="?customer-profile=<?php echo $profile_id; ?>" method="POST" enctype="multipart/form-data">
    <input type="hidden" value="<?php echo $profile_id; ?>" id="form-profile-id" name="form-profile-id" />
    <div class="row">
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Overview</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                <div class="box-body">
                    <div class="form-group">
                        <label>Title</label>
                        <select class="form-control select2" id="gender" name="gender">
                            <option value="" <?php
                            if ($user->gender === "") {
                                echo 'selected="selected"';
                            }
                            ?>>(Unspecified)</option>
                            <option value="Mr." <?php
                            if ($user->gender === "Mr.") {
                                echo 'selected="selected"';
                            }
                            ?>>Mr.</option>
                            <option value="Mrs." <?php
                            if ($user->gender === "Mrs.") {
                                echo 'selected="selected"';
                            }
                            ?>>Mrs.</option>
                            <option value="Ms." <?php
                            if ($user->gender === "Ms.") {
                                echo 'selected="selected"';
                            }
                            ?>>Ms.</option>
                            <option value="Dr." <?php
                            if ($user->gender === "Dr.") {
                                echo 'selected="selected"';
                            }
                            ?>>Dr.</option>
                        </select>
                    </div><!-- /.form-group -->
                    <div class="form-group">
                        <label for="fname">First Name *</label>
                        <input required="true" class="form-control" id="fname" value="<?php echo htmlspecialchars($user->fname); ?>" name="fname" placeholder="Enter first name" type="text">
                    </div>
                    <div class="form-group">
                        <label for="lname">Last Name *</label>
                        <input required="true" class="form-control" id="lname" value="<?php echo htmlspecialchars($user->lname); ?>" name="lname" placeholder="Enter last name" type="text">
                    </div>
                    <div class="form-group">
                        <label for="picture">Avatar</label>
                        <p class="help-block"><?php
                            if ($user->picture) {
                                echo "<img class=\"img-circle\" style=\"width: 90px;height: 90px;\" src=\"$user->picture\" alt=\"Avatar\" />";
                            } else {
                                echo 'Please pick a user profile picture.';
                            }
                            ?></p>
                        <input id="picture" name="picture" type="file" accept="image/gif, image/jpeg, image/png" >

                    </div>
                </div><!-- /.box-body -->

            </div><!-- /.box -->
        </div>
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Contact and Basic Info</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                <div class="box-body">
                    <div class="form-group">
                        <label for="email">Email address *</label>
                        <input required="true" class="form-control" value="<?php echo htmlspecialchars($user->email); ?>" id="email" name="email" placeholder="Enter email" type="email">
                    </div>
                    <div class="form-group">
                        <label for="fname">City</label>
                        <input class="form-control" id="city" value="<?php echo htmlspecialchars($user->city); ?>" name="city" placeholder="Enter city" type="text">
                    </div>
                    <div class="form-group">
                        <label for="country">Country</label>
                        <select class="form-control select2" id="country" name="country">
                            <?php
                            $countries = $db->query('SELECT `country` from `country` ORDER BY `priority`, `country`');
                            if ($countries) {
                                while ($country = $countries->fetch_object()) {
                                    echo '<option value="' . htmlspecialchars($country->country) . '"';
                                    if ($user->country === $country->country) {
                                        echo ' selected';
                                    }
                                    echo '>' . htmlspecialchars($country->country) . '<option>';
                                }
                            }
                            ?>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="contact">Contact</label>
                        <input class="form-control" type="tel" id="contact" value="<?php echo htmlspecialchars($user->contact); ?>" name="contact" placeholder="Enter contact number" type="text">
                    </div>
                </div><!-- /.box-body -->
            </div><!-- /.box -->
        </div>    
    </div>
    <?php if ($profile_id > 0) { ?>
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
                            $sql = "SELECT  * from `notification_event`
                                WHERE `udid` = '$user->udid'
                            ORDER BY  `time` DESC 
                            LIMIT 20";
                            $notification_info_query = $db->query($sql);
                            if($notification_info_query->num_rows <= 0) {
                                echo "<p>(No Activities)</p>";
                            }
                            if ($notification_info_query) {
                                while ($notification = $notification_info_query->fetch_object()) {
                                    ?>
                                    <li class="item">
                                        <div >
                                            <a href="javascript::;" class="product-title"><?php echo htmlspecialchars($notification->title); ?></a>
                                            <span class="product-description">
                                                <?php echo htmlspecialchars($notification->description); ?>
                                                <br>
                                                <?php echo \utils\humanTiming($notification->time); ?>
                                            </span>
                                        </div>
                                    </li><!-- /.item -->
                                    <?php
                                }
                                $notification_info_query->close();
                            }
                            ?>
                        </ul>
                    </div><!-- /.box-body -->
                    <!--<div class="box-footer text-center">
                        <a href="javascript::;" class="uppercase">View All Activities</a>
                    </div>--><!-- /.box-footer -->
                </div>
            </div>
        </div>
    <?php } ?>
    <!-- Buttons -->
    <div class="row no-print">
        <div class="col-xs-12">
            <a href="#" target="_blank" class="btn btn-default disabled"><i class="fa fa-print"></i> Print</a>
            <button type="submit" id="submit" name="submit" class="btn btn-success pull-right "><i class="fa fa-save"></i> Save Profile</button>
            <a href="./?users" class="btn btn-defaul pull-right" style="margin-right: 5px;"><i class="fa fa-close"></i> Discard Changes</a>
        </div>
    </div>
</form>