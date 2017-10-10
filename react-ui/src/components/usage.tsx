import * as React from 'react';
import { Well } from 'react-bootstrap';

class Usage extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            usage: { CpuPercent: 0, BytesUsed: 0 }
        };
    }

    proxy: SignalR.Hub.Proxy;

    handleUpdateUsage = (u: any) => {
        this.setState( {usage : u} );
    }

    componentDidMount() {

        // Configure Connection
        $.connection.hub.url = 'http://localhost:8080/signalr';
    
        // Turn on logging
        $.connection.hub.logging = true;

        // Create dynamic proxy for usageHub
        this.proxy = $.connection.hub.createHubProxy('usageHub');
        this.proxy.on('updateUsage', (usage) => {
            this.handleUpdateUsage(usage);
        });

        // Start the connection
        $.connection.hub.start().done(() => {
            this.proxy.invoke('register').done((usage: any) => {
                this.handleUpdateUsage(usage);    
            });
        });
    }

    componentWillUnMount() {
        this.proxy.off('updateUsage', (usage: any) => {
            this.setState({ usage: { CpuPercent: 0, BytesUsed: 0 } });
        });
    }

    render() {
        return (
            <div>
                <h2>Current Usage</h2>
                <Well>CPU: {this.state.usage.CpuPercent * 100} %</Well>
                <Well>RAM: {Math.round((this.state.usage.BytesUsed / 1024 / 1024) * 100) / 100} GB</Well>
            </div>
        );
    }
}

export default Usage;