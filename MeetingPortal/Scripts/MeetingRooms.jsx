class MeetingTable extends React.Component {
    render() {
        const roomNodes = this.props.data.map(room => (
            <MeetingRoom data={room} key={room.Id} />
        ));
        return (
            <table className="table table-striped">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col">
                            Название комнаты
                        </th>
                        <th scope="col">
                            Количество кресел
                        </th>
                        <th scope="col">
                            Наличие проектора
                        </th>
                        <th scope="col">
                            Наличие маркерной доски
                        </th>
                        <th scope="col">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {roomNodes}
                </tbody>
            </table>
        );
    }
}

class MeetingRoom extends React.Component {
    getIcon(b) {
        if (b) {
            return (<i className="fa fa-check text-success"></i>);
        }
        return (<i className="fa fa-times text-danger"></i>);
    }
    render() {
        return (
            <tr>
                <td>{this.props.data.Name}</td>
                <td>{this.props.data.NumberOfChair}</td>
                <td>{this.getIcon(this.props.data.HaveMarkerBoard)}</td>
                <td>{this.getIcon(this.props.data.HaveProjector)}</td>
                <td>
                    <a href={"/home/EditMeetingRoom/" + this.props.data.Id} className="btn btn-primary">Редактировать</a>
                </td>
            </tr>
        );
    }
}

class MeetingBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = { meetingRooms: [] };
    }
    loadMeetingRoomsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ meetingRooms: data });
        };
        xhr.send();
    }
    componentDidMount() {
        this.loadMeetingRoomsFromServer();
    }
    render() {
        return (
            <div className="meeting-box animated fadeInUp">
                <h2>Список переговорных</h2>
                <MeetingTable data={this.state.meetingRooms} />
            </div>
        );
    }
}

ReactDOM.render(
    <MeetingBox url="home/rooms" />, document.getElementById('meetingRooms')
);