<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

session_start();
$start = intval($start = filter_input(INPUT_GET, 'start'));
$length = intval($length = filter_input(INPUT_GET, 'length'));
//filter_input_array
$search = $_GET['search'];//filter_input(INPUT_GET, "search");
$order = $_GET['order'];
$columns = $_GET['columns'];

if(!isset($_SESSION[MEMBER_ID])) { 
    echo '{ "msg": "Goodbye, buddy!" }';
    exit;
}

$db = database\getMySqli();

