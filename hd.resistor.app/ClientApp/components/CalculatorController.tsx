








import * as React from "react";
import { RouteComponentProps } from "react-router";
import "isomorphic-fetch";
import { ResistorColor } from "./ResistorColor";

import { ColorsController_Get } from "./ColorsController";





interface number_State {
    bands: string[];
    loading: boolean;
    selected: number;
    result: number;
}
export class CalculatorController_CalculateOhmValue extends React.Component<RouteComponentProps<{}>, number_State> {
    constructor() {
        super();
        this.state = { bands: ["", "", "", ""], result: 0, selected: 0, loading: false };
    }

    public go() {
        this.setState({ loading: true });

        var args = this.state.bands.slice();;
        var url = 'api/Calculator?a='.concat(args[0]).concat(
                                '&b=').concat(args[1]).concat(
                                '&c=').concat(args[2]).concat(
                                '&d=').concat(args[3]);
        fetch(url)
            .then(response => response.json() as Promise<number>)
            .then(data => {
                this.setState({ result: data, loading: false });
            });
    }
    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <p><b>Result: </b><em></em> Ohms</p>

        return <div className='row'>
            <div className='col-sm-12'>
                {this.renderTable(this.state.bands)}
            </div>
            <div className='col-sm-9'>
                <button
                    onClick={(e) => { this.setState({ bands: ["", "", "", ""] }); e.stopPropagation(); }}>
                    Reset
                </button>
                <button
                    onClick={(e) => { this.go(); e.stopPropagation(); }}>
                    Calculate...
                </button>
                <p><b><em>Result:</em></b> {this.state.result} <i>Ohms</i></p>
            </div>
            </div>;
    }
    private setColor(name: string)
    {
        var color = name;
        const data = this.state.bands.slice();
        var selected = this.state.selected;

        data[selected] = color;
        this.setState({ bands:data});
    }

    private renderTable(data: string[]) {
        var header = data.slice(0);
        var columns = data.slice(0);
        var selected = this.state.selected;
        var ugly = (selected == 0) ? 'outset' : 'inset';

        return <table>
            <thead>
                {header.map((x, i) =>
                    <th style={{ textOverflow: 'none', textAlign: 'center' }}>{ String.fromCharCode('A'.charCodeAt(0)+i) }</th>
                )}
                <th style={{ paddingLeft: '10px', textOverflow: 'none', textAlign: 'center' }}>Color Type</th>
            </thead>
            <tbody>
                <tr style={{ verticalAlign: 'top' }}>
                    {columns.map((x, j) =>
                        <td style={{ borderStyle: { ugly }, border: '1px 1px 1px 1px' }}>
                            <button
                                key={j}
                                onClick={(e) => { this.setState({ selected: j }); e.stopPropagation(); }}                                
                                style={{ width: '50px', height: '50px', backgroundColor: this.state.bands[j] }}>
                                {this.state.bands[j]}
                            </button>
                        </td>
                    )}
                    <td style={{ paddingLeft: '10px', textOverflow: 'none', textAlign: 'center' }}><ColorsController_Get setColor={color => { this.setColor(color); }}/></td>
                </tr>
            </tbody>
        </table>;
    }
}

