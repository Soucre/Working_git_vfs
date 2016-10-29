<!-- @@TITLE=Profile-->
<!-- @@DESCRIPTION=Profile of user-->
<!-- @@BREADCRUMB=Administration#Profile-->
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
    $address = $db->real_escape_string(filter_input(INPUT_POST, "address"));
    $role = $db->real_escape_string(filter_input(INPUT_POST, "role"));

    $new_password = filter_input(INPUT_POST, "new_password");
    $re_type_password = filter_input(INPUT_POST, "re_type_password");

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
    $set_statement = "SET `email` = '$email', `fname` = '$fname', `lname`='$lname', gender='$gender', address='$address', role='$role'";
    if ($uploadOk && file_exists($target_file)) {
        $set_statement .= ", picture = '$target_file'";
    } else {
        $uploadOk = false;
    }

    $password_msg = "";
    //Password
    if ($new_password === $re_type_password) {// && strlen($new_password) > 0) {
        $new_password = md5($new_password);
        $set_statement .= ", password = '$new_password'";
        $password_msg = "<br><b>Password has been changed.</b>";
    }

    if ($profile_id == 0) {
        // Create new
        $sql = "INSERT INTO `member` $set_statement";
    } else {
        // Save current
        $sql = "UPDATE `member` $set_statement WHERE `mem_id` = $profile_id";
    }
    $db->query($sql);
    if ($db->affected_rows >= 1) {
        if ($profile_id == 0) {
            $profile_id = $db->insert_id;
        }
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($fname) . "'s profile has been saved." . $password_msg;
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "success";
        if ($profile_id === $_SESSION[MEMBER_ID]) {
            // Reload session data
            $_SESSION[MEMBER_EMAIL] = filter_input(INPUT_POST, "email");
            $_SESSION[MEMBER_DISPLAY_NAME] = filter_input(INPUT_POST, "fname") . " " . filter_input(INPUT_POST, "lname");
            if ($uploadOk) {
                $_SESSION[MEMBER_PICTURE] = $target_file;
            }
            $_SESSION[MEMBER_ROLE] = $role;
        }
        /*         * ?>
          <script type="text/javascript">
          window.location = "./?profile=<?php echo $profile_id; ?>"
          </script>
          <?php
          exit;* */
    } else {
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($fname) . "'s profile has not been saved." . $file_msg;
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "warning";
    }
    $_SESSION[APPLICATION_MESSAGE] .= " What do you want to do next?<br>"
            . "Continue editing, <a href=\"./?profile\">create new profile</a>, or go back to <a href=\"./?users\">User Management</a>.";
} else {
    $profile_id = filter_input(INPUT_GET, "profile");
}

$found = false;
if ($profile_id > 0) {
    $result = $db->query("SELECT * FROM `member` WHERE `mem_id` = $profile_id");
    if ($result) {
        $user = $result->fetch_object();
        $found = true;
    }
    if (!$found) {
        $_SESSION[APPLICATION_MESSAGE] = "Profile #$profile_id not found. You may not have the right to view, or it has been deleted.";
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "danger";
    }
    $result->close();
    echo '<script>contentHeaderTitle_SetContent("' . htmlspecialchars($user->fname . ' ' . $user->lname) . '<small>Profile of user</small>")</script>';
}

include './app/form-notification.php';
?>

<form role="form" action="./?profile=<?php echo $profile_id; ?>" method="POST" enctype="multipart/form-data">
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
                        <label for="fname">First Name *</label>
                        <input required="true" class="form-control" id="fname" value="<?php echo htmlspecialchars($user->fname); ?>" name="fname" placeholder="Enter first name" type="text">
                    </div>
                    <div class="form-group">
                        <label for="lname">Last Name *</label>
                        <input required="true" class="form-control" id="lname" value="<?php echo htmlspecialchars($user->lname); ?>" name="lname" placeholder="Enter last name" type="text">
                    </div>
                    <div class="form-group">
                        <label>Gender</label>
                        <select class="form-control select2" id="gender" name="gender">
                            <option value="" <?php
                            if ($user->gender === "") {
                                echo 'selected="selected"';
                            }
                            ?>>(Unspecified)</option>
                            <option value="Male" <?php
                            if ($user->gender === "Male") {
                                echo 'selected="selected"';
                            }
                            ?>>Male</option>
                            <option value="Female" <?php
                            if ($user->gender === "Female") {
                                echo 'selected="selected"';
                            }
                            ?>>Female</option>
                        </select>
                    </div><!-- /.form-group -->
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
                        <label for="fname">Address</label>
                        <input class="form-control" id="address" value="<?php echo htmlspecialchars($user->address); ?>" name="address" placeholder="Enter address" type="text">
                    </div>
                </div><!-- /.box-body -->
            </div><!-- /.box -->
            <!-- account -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Account</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                <div class="box-body">
                    <div class="form-group">
                        <label>Role *</label>
                        <select class="form-control select2" id="role" name="role">
                            <?php
                            $result = $db->query("SELECT * from `role` order by `role_name`");
                            if ($result) {
                                while ($_role = $result->fetch_object()) {
                                    $_description = "";
                                    if(strlen($_role->description) > 0) {
                                        $_description = "($_role->description)";
                                    }
                                    echo "<option value=\"$_role->role_name\"";
                                    if ($user->role === $_role->role_name) {
                                        echo " selected=\"selected\" ";
                                    }
                                    echo ">" . htmlspecialchars($_role->role_name) . " " . htmlspecialchars($_description) . "</option>";
                                }
                            }
                            $result->close();
                            ?>                            
                        </select>
                    </div><!-- /.form-group -->
                    <div class="form-group">
                        <label for="new_password">New password *</label>
                        <input class="form-control" id="new_password" name="new_password" placeholder="Enter new password" type="password">
                    </div>
                    <div class="form-group">
                        <label for="re_type_password">Re-type password *</label>
                        <input class="form-control" id="re_type_password" name="re_type_password" placeholder="Re-type new password" type="password">
                    </div>
                </div><!-- /.box-body -->
            </div><!-- /.box -->
        </div>    
    </div>
    <!-- Buttons -->
    <div class="row no-print">
        <div class="col-xs-12">
            <a href="#" target="_blank" class="btn btn-default disabled"><i class="fa fa-print"></i> Print</a>
            <button type="submit" id="submit" name="submit" class="btn btn-success pull-right "><i class="fa fa-save"></i> Save Profile</button>
            <a href="./?users" class="btn btn-default pull-right" style="margin-right: 5px;"><i class="fa fa-close"></i> Discard Changes</a>
        </div>
    </div>
</form>
