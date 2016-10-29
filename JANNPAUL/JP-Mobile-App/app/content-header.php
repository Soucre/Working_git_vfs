<!-- Content Header (Page header) -->
<section class="content-header">
    <h1 id="content-header-title-id">
        <?php echo $_SESSION[PAGE_TITLE]; ?>
        <small><?php echo $_SESSION[PAGE_DESCRIPTION]; ?></small>
    </h1>
    <ol class="breadcrumb" id="content-header-breadcrumb-id">
        <?php echo $_SESSION[BREADCRUMB]; ?>
    </ol>    
</section>
<script>

function contentHeaderTitle_SetContent($content) {
    $( "#content-header-title-id").html($content);
}

function contentHeaderTitle_SetBreadcrumb($content) {
    $( "#content-header-breadcrumb-id").html($content);
}


</script>