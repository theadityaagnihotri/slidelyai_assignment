import { Router } from 'express';
import fs from 'fs';

const router = Router();
const dbFilePath = './db.json';

router.post('/', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;

    if (!name || !email || !phone || !github_link || !stopwatch_time) {
        return res.status(400).json({ error: 'All fields are required' });
    }

    const newSubmission = { name, email, phone, github_link, stopwatch_time };

    const dbData = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    dbData.submissions.push(newSubmission);
    fs.writeFileSync(dbFilePath, JSON.stringify(dbData, null, 2));

    res.status(201).json(newSubmission);
});

export default router;
