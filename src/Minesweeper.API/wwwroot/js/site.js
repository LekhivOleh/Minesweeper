document.addEventListener('DOMContentLoaded', () => {
    attachBoardHandlers();
});

function attachBoardHandlers() {
    const board = document.getElementById('game-board');
    if (!board) return;

    board.replaceWith(board.cloneNode(true));
    const newBoard = document.getElementById('game-board');

    newBoard.addEventListener('click', e => {
        if (e.target.classList.contains('cell')) {
            const rowClass = Array.from(e.target.classList).find(c => c.startsWith('row-'));
            const colClass = Array.from(e.target.classList).find(c => c.startsWith('col-'));
            if (rowClass && colClass) {
                const row = parseInt(rowClass.replace('row-', ''));
                const col = parseInt(colClass.replace('col-', ''));
                reveal(row, col);
            }
        }
    });

    newBoard.addEventListener('contextmenu', e => {
        if (e.target.classList.contains('cell')) {
            e.preventDefault();
            const rowClass = Array.from(e.target.classList).find(c => c.startsWith('row-'));
            const colClass = Array.from(e.target.classList).find(c => c.startsWith('col-'));
            if (rowClass && colClass) {
                const row = parseInt(rowClass.replace('row-', ''));
                const col = parseInt(colClass.replace('col-', ''));
                flag(row, col);
            }
        }
    });

    newBoard.addEventListener('mousedown', e => {
        if (e.button === 1 && e.target.classList.contains('cell')) {
            e.preventDefault();
            const rowClass = Array.from(e.target.classList).find(c => c.startsWith('row-'));
            const colClass = Array.from(e.target.classList).find(c => c.startsWith('col-'));
            if (rowClass && colClass) {
                const row = parseInt(rowClass.replace('row-', ''));
                const col = parseInt(colClass.replace('col-', ''));
                question(row, col);
            }
        }
    });
}


function updateStatusAlert() {
    axios.get('/Game/GameAlert')
        .then(res => {
            document.getElementById('status-alert').innerHTML = res.data;
        })
        .catch(err => {
            console.error('Status error:', err)
        });
}

function reveal(row, col) {
    axios.post('/Game/RevealCell', { row, col })
        .then(res => {
            document.getElementById('game-board-container').innerHTML = res.data;
            attachBoardHandlers();
            updateStatusAlert();
        })
        .catch(err => {
            console.error('Reveal error:', err)
        });
}

function flag(row, col) {
    axios.post('/Game/FlagCell', { row, col })
        .then(res => {
            document.getElementById('game-board-container').innerHTML = res.data;
            attachBoardHandlers();
            updateStatusAlert();
        })
        .catch(err => console.error('Flag error:', err));
}

function question(row, col) {
    axios.post('/Game/QuestionCell', { row, col })
        .then(res => {
            document.getElementById('game-board-container').innerHTML = res.data;
            attachBoardHandlers();
        })
        .catch(err => console.error('Question error:', err));
}