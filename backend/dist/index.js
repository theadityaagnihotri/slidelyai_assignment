"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const ping_1 = __importDefault(require("./routes/ping"));
const submit_1 = __importDefault(require("./routes/submit"));
const read_1 = __importDefault(require("./routes/read"));
const app = (0, express_1.default)();
const PORT = 3000;
app.use(body_parser_1.default.json());
app.use('/ping', ping_1.default);
app.use('/submit', submit_1.default);
app.use('/read', read_1.default);
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
