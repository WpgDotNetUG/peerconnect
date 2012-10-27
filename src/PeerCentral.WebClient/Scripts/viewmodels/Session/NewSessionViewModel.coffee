class window.NewSesionViewModel

	constructor: ->
		$('#users li a').on 'click', @loginUser

	loginUser: (e) ->
		userId = $(e.target).attr('data-id')
		$.post "/session", {id: userId}, (data) -> window.location.replace("/");
		

$ ->
	new window.NewSesionViewModel()