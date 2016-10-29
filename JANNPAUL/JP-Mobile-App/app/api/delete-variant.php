<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

//Ajax calling does not pass the session data to
/* if(!isset($_SESSION[MEMBER_ID])) {
  return;
  } */

if (!filter_has_var(INPUT_GET, "variant_id")) {
    return;
}

$db = \database\getMySqli();
$db->query("DELETE FROM `product_variant` WHERE `variant_id` = " . filter_input(INPUT_GET, "variant_id"));


