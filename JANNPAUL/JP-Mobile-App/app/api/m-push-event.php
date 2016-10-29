<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';


if (filter_has_var(INPUT_GET, "save")) {
    // Save post
    $db = database\getMySqli();

    $udid = filter_input(INPUT_GET, "udid");
    $title = $db->real_escape_string(filter_input(INPUT_GET, "title"));
    $description = $db->real_escape_string(filter_input(INPUT_GET, "description"));
    $product_id = $db->real_escape_string(filter_input(INPUT_GET, "product_id"));

    if (!isset($product_id) || trim($product_id) === "" || strlen($product_id) <= 0) {
        $product_id = "NULL";
    }
    $sql = "INSERT INTO `notification_event`(`udid`, `title`, `description`, `product_id`) values('$udid', '$title', '$description', $product_id)";
    $db->query($sql);

    $saveResult = new \database\SaveResult($db, $sql);
    $saveResult->toJson();
} 
