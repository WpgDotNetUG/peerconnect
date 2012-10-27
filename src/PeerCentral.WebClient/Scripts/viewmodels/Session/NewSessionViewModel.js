(function() {

  window.NewSesionViewModel = (function() {

    function NewSesionViewModel() {
      $('#users li a').on('click', this.loginUser);
    }

    NewSesionViewModel.prototype.loginUser = function(e) {
      var userId;
      userId = $(e.target).attr('data-id');
      return $.post("/session", {
        id: userId
      }, function(data) {
        return window.location.replace("/");
      });
    };

    return NewSesionViewModel;

  })();

  $(function() {
    return new window.NewSesionViewModel();
  });

}).call(this);
