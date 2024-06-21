"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const fs_1 = __importDefault(require("fs"));
const router = (0, express_1.Router)();
const dbFilePath = './db.json';
router.post('/', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    if (!name || !email || !phone || !github_link || !stopwatch_time) {
        return res.status(400).json({ error: 'All fields are required' });
    }
    const newSubmission = { name, email, phone, github_link, stopwatch_time };
    const dbData = JSON.parse(fs_1.default.readFileSync(dbFilePath, 'utf-8'));
    dbData.submissions.push(newSubmission);
    fs_1.default.writeFileSync(dbFilePath, JSON.stringify(dbData, null, 2));
    res.status(201).json(newSubmission);
});
exports.default = router;
