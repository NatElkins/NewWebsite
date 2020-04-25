// Learn more about F# at http://fsharp.org

open System
open Saturn
open Giraffe
open Giraffe.GiraffeViewEngine

let [<Literal>] SkeletonCSSUrl = "https://cdnjs.cloudflare.com/ajax/libs/skeleton/2.0.4/skeleton.min.css"


[<EntryPoint>]
let main argv =

    let refreshJs = 
        """
        var socket = new WebSocket('ws://localhost:5000/ws');
        socket.onopen = function(event) {
          console.log('Connection opened');
        }
        socket.onmessage = function(event) {
          console.log(event.data);
          window.location.reload();
          return false;
        }
        socket.onclose = function(event) {
          console.log("connection closed");
        }
        socket.onerror = function(error) {
          console.log("error", error);
        }
        """


    let mainView = 
        html [] [
            head [] [
                //script [] [ Text refreshJs ] 
                meta [ _name "viewport"; _content "width=device-width, initial-scale=1" ]
                meta [ _name "description"; _content "The blog of Nat Elkins"]
                meta [ _httpEquiv "content-type"; _content "text/html; charset=utf-8"]
                link [
                    _rel "stylesheet"
                    _href "https://unpkg.com/purecss@1.0.1/build/pure-min.css"
                    _integrity "sha384-oAOxQR6DkCoMliIh8yFnu25d7Eq/PHS21PClpwjOTeU2jRSq11vu66rf90/cZr47"
                    _crossorigin "anonymous"
                ] 
            ]
            body [] [
                div [ _class "pure-g" ] [
                    div [ _class "pure-u-1-3"] [ p [] [ Text "Thirds"]]
                    div [ _class "pure-u-1-3"] [ p [] [ Text "Thirds"]]
                    div [ _class "pure-u-1-3"] [ p [] [ Text "Thirds"]]
                ] 
            ]
        ]
    
    printfn "%s" <| (renderHtmlDocument <| mainView)

    let rtr = router {
        get "/" (htmlView mainView)
    }
    let app = application {
        use_router rtr
    }
    run app
    0 // return an integer exit code
