import * as React from 'react';
// import * as $ from 'jquery';

class Usage extends React.Component {
    proxy: SignalR.Hub.Proxy;

    componentDidMount() {
        // var handler = this.updateStockHandler;
        // var allHandler = this.updateAllStocksHandler;

        // $.connection.hub.url = 'http://localhost:8080/signalr';
        
        // Add a client-side hub method that the server will call
    
        // Turn on logging
        // $.connection.hub.logging = true;
    
        // Start the connection
        // this.proxy = $.connection.hub.createHubProxy("usageHub");
        // this.proxy.on("updateUsage", (usage: any) => {

        // });

        // var p = this.proxy;
        // $.connection.hub.start().done(function() {
        //     p.invoke("register").done(function(usage: any) {

        //     });
        // });
    }

    componentWillUnMount() {
        // this.proxy.off("updateUsage", (usage: any) => {
            
        // });
    }

    render() {
        return (
            <div>Usage
            </div>
        );
    }
}

export default Usage;