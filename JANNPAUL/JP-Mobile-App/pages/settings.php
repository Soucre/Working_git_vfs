<!-- @@TITLE=Settings-->
<!-- @@DESCRIPTION=Application system settings-->
<!-- @@BREADCRUMB=Administration#Settings-->
<?php
$db = database\getMySqli();

if (filter_has_var(INPUT_POST, "submit")) {
    //Save mode
    $settings = $db->query("SELECT * FROM `setting`");
    if ($settings) {
        $sql = "";
        $afftected_count = 0;
        while ($setting = $settings->fetch_object()) {
            $value = $db->real_escape_string(filter_input(INPUT_POST, $setting->key));
            // Save current
            $sql = "UPDATE `setting` SET `value` = '$value' WHERE `key` = '$setting->key'";
            $db->query($sql);
            $afftected_count += $db->affected_rows;
        }
        //echo "<p>$sql</p>";        
        if ($afftected_count > 0) {
            $_SESSION[APPLICATION_MESSAGE] = "Settings has been saved.";
            $_SESSION[APPLICATION_MESSAGE_TYPE] = "success";

            $_SESSION[$setting->key] = filter_input(INPUT_POST, $setting->key);
        } else {
            $_SESSION[APPLICATION_MESSAGE] = "Settings has not been saved.";
            $_SESSION[APPLICATION_MESSAGE_TYPE] = "warning";
        }
        $_SESSION[APPLICATION_MESSAGE] .= " What do you want to do next?<br>"
                . "Continue editing or go back to <a href=\"./\">home page</a>.";
    }
    $settings->close();
}

include './app/form-notification.php';

$settings_controls = $db->query("SELECT * FROM `setting` order by `group_name`, `order_by`");
?>

<?php

function createSettingItem($groupName) {
    ?>
    <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title"><?php echo htmlspecialchars($groupName); ?></h3>
        </div><!-- /.box-header -->
        <!-- form start -->
        <div class="box-body">
            <?php
            global $settings_controls;
            if ($settings_controls) {
                $settings_controls->data_seek(0);
                while ($setting = $settings_controls->fetch_object()) {
                    if ($setting->group_name === $groupName) {
                        ?>
                        <div class="form-group">
                            <label for="<?php echo htmlspecialchars($setting->key); ?>"><?php echo htmlspecialchars($setting->title) . ($setting->required ? ' *' : ''); ?></label>
                            <input <?php
                            echo ($setting->required) ? 'required="true"' : '';
                            ?>  <?php echo ($setting->regex ? 'pattern="' . htmlspecialchars($setting->regex) . '"' : ''); ?> 
                                class="form-control" id="<?php echo htmlspecialchars($setting->key); ?>" 
                                name="<?php echo htmlspecialchars($setting->key); ?>" 
                                value="<?php echo htmlspecialchars($setting->value); ?>" 
                                placeholder="<?php echo htmlspecialchars($setting->description); ?>" type="text">
                        </div>
                        <?php
                    }
                }
            }
            ?>
        </div><!-- /.box-body -->
    </div><!-- /.box -->    
    <?php
}
?>

<form role="form" action="./?settings" method="POST" enctype="multipart/form-data">
    <div class="row">        
        <div class="col-md-6">
            <!-- general elements -->
            <?php createSettingItem("General"); ?>
        </div>    
        <div class="col-md-6">
            <!-- application elements -->
            <?php createSettingItem("Application"); ?>
        </div>  
        <div class="col-md-6">
            <!-- display format form elements -->
            <?php createSettingItem("Display Format"); ?>
        </div>  
        <div class="col-md-6">
            <!-- mobility elements -->
            <?php createSettingItem("Mobile"); ?>
        </div>  
    </div>
    <!-- Buttons -->
    <div class="row no-print">
        <div class="col-xs-12">
            <button type="submit" id="submit" name="submit" class="btn btn-success pull-right "><i class="fa fa-save"></i> Save Settings</button>
            <a href="./" class="btn btn-default pull-right" style="margin-right: 5px;"><i class="fa fa-close"></i> Discard Changes</a>
        </div>
    </div>
</form>

<?php
$settings_controls->close();
