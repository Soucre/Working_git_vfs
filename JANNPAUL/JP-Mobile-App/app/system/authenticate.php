<?php
namespace authenticate;

/**
 * Check if the current logged-in user has the right to read current object
 * The object is the included full path file name, for ex: './pages/users.php', './pages/profile.php'...
 * @param string $object current module to check
 * @return true if the user is allowed to access
 */
function auth_can_read($object) {
    $role = $_SESSION[MEMBER_ROLE];
    //Default roles
    if ($role === APPLICATION_ROLE_ADMINISTRATOR) {
        return true;
    }
    if (strpos($object, "pages/dashboard")) {
        return true;
    }
    $db = \database\getMySqli();
    $sql = "SELECT * from `module_role` WHERE `module_name` = '$object' AND `role_name` = '$role'";
    $result = $db->query($sql);
    if ($result) {
        $roles = $result->fetch_object();
        $result->close();
        return $roles->read;
    } else {
       
    }

    return false;
}