<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

//Ajax calling does not pass the session data to
/* if(!isset($_SESSION[MEMBER_ID])) {
  return;
  } */

if (!filter_has_var(INPUT_GET, "product_id_1") || !filter_has_var(INPUT_GET, "product_id_2")) {
    return;
}

$db = \database\getMySqli();
$db->query("INSERT INTO `similar_product` SET `product_id_1` = " .
        filter_input(INPUT_GET, "product_id_1") . ", `product_id_2` = " . filter_input(INPUT_GET, "product_id_2"));


