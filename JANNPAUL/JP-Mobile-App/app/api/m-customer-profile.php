<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

$udid = filter_input(INPUT_GET, "udid");

if (filter_has_var(INPUT_GET, "save")) {
    // Save post
    $db = database\getMySqli();

    $member_id = filter_input(INPUT_GET, "mem_id");

    $update_sql = "`city` = '" . $db->real_escape_string(filter_input(INPUT_GET, "city")) . "', " .
            "`contact` = '" . $db->real_escape_string(filter_input(INPUT_GET, "contact")) . "', " .
            "`country` = '" . $db->real_escape_string(filter_input(INPUT_GET, "country")) . "', " .
            "`email` = '" . $db->real_escape_string(filter_input(INPUT_GET, "email")) . "', " .
            "`fname` = '" . $db->real_escape_string(filter_input(INPUT_GET, "fname")) . "', " .
            "`gender` = '" . $db->real_escape_string(filter_input(INPUT_GET, "gender")) . "', " .
            "`lname` = '" . $db->real_escape_string(filter_input(INPUT_GET, "lname")) . "'";
    $sql = "";
    if ($member_id <= 0) {
        $sql = "INSERT INTO `customer_profile` SET `udid` = '$udid', " . $update_sql;
    } else {
        $sql = "UPDATE `customer_profile` SET " .
                $update_sql . " WHERE `udid` = '$udid'";
    }

    //echo $sql;
    $result = $db->query($sql);

    $saveResult = new \database\SaveResult($db, $sql);
    $saveResult->toJson();

    $title = $db->real_escape_string($member_id <= 0 ? "Register new profile" : "Update profile");
    $description = $db->real_escape_string(filter_input(INPUT_GET, "fname") . " has updated the profile.");

    $db->query("INSERT INTO `notification_event`(`udid`, `title`, `description`) values('$udid', '$title', '$description')");
} else {

    class CustomerProfile {

        public $count = 0;
        public $data = Array();

        public function __construct() {
            $db = database\getMySqli();
            global $udid;
            $result = $db->query("SELECT * FROM `customer_profile` WHERE `udid` = '$udid'");
            if ($result) {
                $this->count = $result->num_rows;
                while ($customer_profile = $result->fetch_object()) {
                    array_push($this->data, $customer_profile);
                }
            }
        }

    }

    $customer_profile = new CustomerProfile();
    echo json_encode($customer_profile);
}
