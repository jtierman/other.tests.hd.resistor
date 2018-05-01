class Resistor extends React.Component
{
    constructor(props, context, refs) {
        super(props, context, refs);

        this.state = {
            colors: Array(0).fill({}),
            bands: Array(4).fill({ name: '', rgb: 0xFFFFFF }),
            ohms: -1,
            error: ''
        };
    };

    componentDidMount() {
        window.setTimeout(this.getColors(), this.props.pollInterval);
    }

    getColors() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ colors: data });
        }.bind(this);
        xhr.send();
    };
    calculate() {
        const bands = this.state.bands;

        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.submitUrl, true);
        xhr.onload = function (e) {
            var data = JSON.parse(xhr.responseText);
            this.setState({ ohms: data, error: '' });
        }.bind(this);
        xhr.onerror = function (e) {
            var data = JSON.parse(xhr.responseText);
            this.setState({ ohms: -1, error: data });
        }.bind(this);

        xhr.send(bands[0].name, bands[1].name, bands[2].name, bands[3].name);
    };


    handleSubmit (e) {
        e.preventDefault();
        const bands = this.state.bands;
        if (!bands) {
            return;
        }
        this.props.calculate();
    };


    render() {
        return (
            <div>
                <div>
                    <h3>Resistor Calculator</h3>
                    <form onSubmit={this.handleSubmit}>
                        <Bands colors={this.state.colors} items={this.state.bands} />
                        <input type="submit" value="Calc" />
                    </form>
                </div>
                <div>{this.state.ohms} ohms</div>
                <div>{this.state.error} error</div>
            </div>
        );
    }
}

class Bands extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            bands: Array(4).fill({ name: '', rgb: 0xFFFFFF }),
        };
    }

    handleClick(i) {
        const bands = this.state.bands.slice();

        this.setState({
            bands: bands,
            i: i,
            disabled: false
        });

    }
    handleChange(value) 
    {
        const i = this.state.i;
        var bands = this.state.bands.slice();

        bands[i].name = colors[i].name;
        bands[i].rgb = colors[i].rgb;

        this.setState({
           bands: bands
        });
    }

    renderBand(i) {
        return (
            <Band key={i} value={this.state.bands[i]} onClick={() => this.handleClick(i)} />
        );
    }

    render() {
        return (
            <div>
                <div className="board-row">
                    {this.renderBand(0)}
                    {this.renderBand(1)}
                    {this.renderBand(2)}
                    {this.renderBand(3)}
                </div>
                <div>
                    <ColorSelector colors={this.props.colors} disabled={this.state.disabled} />
                </div>
            </div>
        );
    }
}

class Band extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            key: -1,
            name: "White",
        };
    }

    render() {
        return (
            <button className="square" style={{ bgColor: this.props.value.name }} onClick={() => this.props.onClick}>
                {this.props.key}
            </button>
        );
    }
}


class ColorSelector extends React.Component
{

    constructor(props) {
        super(props);
    }

    render() {
        var items = this.props.colors.map(color =>
            <option style={{ bgColor: color.rgb }}>{color.name}</option>
        );
        return (
            <select onChange={(value) => this.props.handleChange}> 
                {items}
            </select>
        );
    }
}
