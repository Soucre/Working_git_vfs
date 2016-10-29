<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

function base64_to_jpeg($base64_string, $output_file) {
    $ifp = fopen($output_file, "wb");

    $encodedData = str_replace(' ', '+', $base64_string);

    fwrite($ifp, base64_decode($encodedData));
    fclose($ifp);

    echo "file size: " . filesize($output_file);

    return( $output_file );
}

// Handling post data for mobile
if (filter_has_var(INPUT_POST, "file") && filter_has_var(INPUT_POST, "data")) {

    $db = \database\getMySqli();

    $file_name = $db->real_escape_string(filter_input(INPUT_POST, "file"));
    $data = filter_input(INPUT_POST, "data");

    $db->query("delete from `debug`");
    $db->query("insert into `debug`(`text`) values('" . $data . "')");

    echo "FILE SAVED: " . base64_to_jpeg($data, "../../dist/img/try-it-on/$file_name");
    echo "data length: " . strlen($data);
} else {
    echo "NO DATA: Check your post param (no file, no data)";
}