<?php

require_once dirname(__DIR__) . '/api/api-header.php';

//Ajax calling does not pass the session data to
if (!isset($_SESSION[MEMBER_ID])) {
    return;
}

if (!filter_has_var(INPUT_GET, "carat") ||
        !filter_has_var(INPUT_GET, "product_id") ||
        !filter_has_var(INPUT_GET, "diamond_cut") ||
        !filter_has_var(INPUT_GET, "setting")) {
    return;
}

$sql = "INSERT INTO `product_variant`(`product_id`, `carat`, `diamond_cut`, `setting`, `title`) "
        . "VALUES (" . filter_input(INPUT_GET, "product_id") . "," . filter_input(INPUT_GET, "carat") . ",'" .
        $db->real_escape_string(filter_input(INPUT_GET, "diamond_cut")) . "','" .
        $db->real_escape_string(filter_input(INPUT_GET, "setting")) . "','" .
        $db->real_escape_string(filter_input(INPUT_GET, "variant_title")) . "'" .
        ")";

$db->query($sql);

