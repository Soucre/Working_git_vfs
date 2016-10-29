<!-- Sidebar user panel (optional) -->
<div class="user-panel">
    <div class="pull-left image">
        <img src="<?php echo $_SESSION[MEMBER_PICTURE]; ?>" class="img-circle" style="width: 45px; height: 45px;" alt="Avatar" />
    </div>
    <div class="pull-left info">
        <p><a href="./?profile=<?php echo $_SESSION[MEMBER_ID]; ?>"><?php echo $_SESSION[MEMBER_DISPLAY_NAME]; ?></a></p>
        <!-- Status -->
        <?php echo $_SESSION[MEMBER_ROLE]; ?>
    </div>
</div>
