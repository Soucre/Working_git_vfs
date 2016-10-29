<?php

namespace utils;

/**
 * Convert mySql date to formatted date as string
 * @param type $date
 */
function dateToString($date) {
    return date($_SESSION[SETTINGS_DATE_FORMAT], strtotime($date));
}

/**
 * Return escapsed string for json
 * @param string $json
 */
function jsonEscapseString($json) {
    $search = array("\n", "\r", "\u", "\t", "\f", "\b", "/", '"');
    $replace = array("\\\\n", "\\\\r", "\\\\u", "\\\\t", "\\\\f", "\\\\b", "\\/", '\\\\\"');
    return str_replace($search, $replace, $json);
}

/**
 * Gets how long in human time
 * @param datetime $time
 * @return string human time
 */
function humanTiming($time) {
    if(!isset($time) || $time === "" || $time == "0000-00-00 00:00:00") {
        return "";
    }
    
    $time = time() - strtotime($time); // to get the time since that moment
    $time = ($time < 1) ? 1 : $time;
    $tokens = array(
        31536000 => 'year',
        2592000 => 'month',
        604800 => 'week',
        86400 => 'day',
        3600 => 'hour',
        60 => 'minute',
        1 => 'second'
    );

    foreach ($tokens as $unit => $text) {
        if ($time < $unit) {
            continue;
        }
        $numberOfUnits = floor($time / $unit);
        return $numberOfUnits . ' ' . $text . (($numberOfUnits > 1) ? 's' : '');
    }
}

/**
 * File size in human readable
 * @param type $bytes
 * @param type $decimals
 * @return type
 */
function human_filesize($bytes, $decimals = 2) {
    $size = array('B','kB','MB','GB','TB','PB','EB','ZB','YB');
    $factor = floor((strlen($bytes) - 1) / 3);
    return sprintf("%.{$decimals}f", $bytes / pow(1024, $factor)) . @$size[$factor];
}

/**
 * Loads avatar image
 * @param string $avatar_picture path to image
 */
function getAvatarIfAvailableOrShowDefaultIfNull($avatar_picture) {
    $image_url = '';
    if (isset($avatar_picture) && trim($avatar_picture) !== "") {
        $image_url = trim($avatar_picture);
    } else {
        $image_url = './dist/img/avatar.png';
    }
    return $image_url;
}
