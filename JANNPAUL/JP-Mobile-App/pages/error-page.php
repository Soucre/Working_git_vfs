<?php 
    $error = 500;
    if(filter_has_var(INPUT_GET, "error")) {
        $error_code = filter_input(INPUT_GET, "error");
    } else {
        $error_code = $_SESSION[APPLICATION_ERROR_CODE];
    }
    $title = "Oops! Something went wrong.";
    $message = "We will work on fixing that right away.";
    switch($error_code) {
        case 403:
            $title = "Oops! Forbidden.";
            $message = "You are not allowed to access this directory.";
            break;
        case 401:
            $title = "Oops! Unauthorized.";
            $message = "You are not allowed to access <b>" . $_SESSION[PAGE_TITLE] . "</b>.";
            break;
        case 404:
            $title = "Oops! Page not found.";
            $message = "We could not find the page you were looking for.";
            break;
    }
?>

<div class="error-page">
    <h2 class="headline text-yellow"> <?php echo $error_code; ?></h2>
    <div class="error-content">
        <h3><i class="fa fa-warning text-yellow"></i> <?php echo $title; ?></h3>
        <p>
            <?php echo $message; ?><br>
            Meanwhile, you may <b><a href="./">return to home</a></b> or try using the search form.
        </p>
        <form class="search-form">
            <div class="input-group">
                <input type="text" name="search" class="form-control" placeholder="Search" />
                <div class="input-group-btn">
                    <button type="submit" name="submit" class="btn btn-warning btn-flat"><i class="fa fa-search"></i></button>
                </div>
            </div><!-- /.input-group -->
        </form>
    </div><!-- /.error-content -->
</div><!-- /.error-page -->