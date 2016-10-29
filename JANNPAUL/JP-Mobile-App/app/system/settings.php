<?php

$db = \database\getMySqli();
$settings = $db->query("SELECT * FROM `setting`");

if ($settings) {
    while ($setting = $settings->fetch_object()) {
        $_SESSION[$setting->key] = $setting->value;
    }
}

$settings->close();

