<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';


if (filter_has_var(INPUT_GET, "save")) {
    $udid = filter_input(INPUT_GET, "udid");
    $product_id = filter_input(INPUT_GET, 'product_id');
    // Save post
    $db = database\getMySqli();
    $result = $db->query("INSERT INTO `saved_ring` SET `udid` = '$udid', `product_id` = $product_id");

    $affected_rows = $db->affected_rows;
    if (!isset($affected_rows)) {
        $affected_rows = 0;
    }
    
    class SaveResult {

        var $affected_rows = -1;
        var $message = "SERVER: SOMETHING WRONG";

    }

    $saveResult = new SaveResult();
    $saveResult->affected_rows = $affected_rows;
    $saveResult->message = $db->error;

    if ($affected_rows == 1) {
        $product_info_query = $db->query("SELECT * FROM `product` WHERE `product_id` = $product_id");
        if ($product_info_query) {
            $product = $product_info_query->fetch_object();
            $product_info_query->close();

            $title = $db->real_escape_string("Save ring");
            $description = $db->real_escape_string("$product->title has been saved to the wish list.");

            $db->query("INSERT INTO `notification_event`(`udid`, `title`, `description`) values('$udid', '$title', '$description')");
        }
    }

    echo json_encode($saveResult);
} else if (filter_has_var(INPUT_GET, "delete")) {
    //SAMPLE: http://m-app.jannpaul.com/app/api/m-saved-rings.php?udid=F49C7543-A0F3-4DB6-B2C4-6FF2E80B43E2&delete&product_id=45
    $udid = filter_input(INPUT_GET, "udid");
    $product_id = filter_input(INPUT_GET, 'product_id');
    // Save post
    $db = database\getMySqli();
    $result = $db->query("DELETE FROM `saved_ring` WHERE `udid` = '$udid' AND `product_id` = $product_id");

    $affected_rows = $db->affected_rows;
    if (!isset($affected_rows)) {
        $affected_rows = 0;
    }
    
    class SaveResult {

        var $affected_rows = -1;
        var $message = "SERVER: SOMETHING WRONG";

    }

    $saveResult = new SaveResult();
    $saveResult->affected_rows = $affected_rows;
    $saveResult->message = $db->error;

    if ($affected_rows == 1) {
        $product_info_query = $db->query("SELECT * FROM `product` WHERE `product_id` = $product_id");
        if ($product_info_query) {
            $product = $product_info_query->fetch_object();
            $product_info_query->close();

            $title = $db->real_escape_string("Remove Save ring");
            $description = $db->real_escape_string("$product->title has been removed from the wish list.");

            $db->query("INSERT INTO `notification_event`(`udid`, `title`, `description`) values('$udid', '$title', '$description')");
        }
    }

    echo json_encode($saveResult);
} else {

    class SavedRing {

        public $count = 0;
        public $data = Array();

        public function __construct() {
            $db = database\getMySqli();
            $result = $db->query("SELECT * FROM `saved_ring` WHERE `udid` = '" . filter_input(INPUT_GET, "udid") . "'");
            if ($result) {
                $this->count = $result->num_rows;
                while ($saved_ring = $result->fetch_object()) {
                    array_push($this->data, $saved_ring);
                }
            }
        }

    }

    $saved_ring = new SavedRing();
    echo json_encode($saved_ring);
}
