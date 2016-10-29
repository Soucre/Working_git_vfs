<?php

namespace database;

/**
 * Gets Mysqli connection
 * @return \Mysqli
 */
function getMySqli() {
    $db = new \mysqli(MY_SQL_HOST, MY_SQL_USER, MY_SQL_PASSWORD, MY_SQL_DATABASE);
    $db->query("SET CHARACTER SET utf8");
    $db->query("SET NAMES utf8");
    if ($db->connect_errno) {
        die('Connect Error: ' . $db->connect_errno);
    }
    return $db;
}

/**
 * Gets number of records from table
 * @param string $table the table's name
 * @param string $condition condition to select
 * @return int number of recourds
 */
function getCount($table, $condition = "1=1") {
    $count_of_records = 0;
    $db = getMySqli();
    $result = $db->query("SELECT COUNT(*) count from `$table` where $condition");
    if ($result) {
        if (($count = $result->fetch_object()->count) > 0) {
            $count_of_records = $count;
        }
        $result->close();
    }
    return $count_of_records;
}

/**
 * Prints number of records from table
 * @param string $table the table's name
 * @param string $condition condition to select
 */
function printCount($table, $condition = "1=1") {
    $count = getCount($table, $condition);
    if ($count > 0) {
        echo $count;
    }
}

/**
 * Prints number of records from table with span tag
 * @param string $table the table's name
 * @param string $condition condition to select
 */
function printCountWithSpan($table, $condition = "1=1") {
    $count = getCount($table, $condition);
    if ($count > 0) {
        echo '<span class="label label-info pull-right">' . $count . '</span>';
    }
}

class SaveResult {

    var $affected_rows = -1;
    var $result = -1; // Previous version compatible
    var $message = "SERVER: SOMETHING WRONG";

    function __construct($db, $sql) {
        $this->affected_rows = $db->affected_rows;
        $this->result = $db->affected_rows;
        if ($db->error !== "") {
            $this->message = $db->error . $sql;
        } else {
            $this->message = "SERVER OK";
        }
    }

    function toJson() {
        echo json_encode($this);
    }

}
